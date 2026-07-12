param()

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

Add-Type -AssemblyName PresentationFramework
Add-Type -AssemblyName PresentationCore
Add-Type -AssemblyName WindowsBase
Add-Type -AssemblyName UIAutomationClient
Add-Type -AssemblyName UIAutomationTypes

$uygulamaDizini = Split-Path -Parent $MyInvocation.MyCommand.Path
$uygulamaYolu = Join-Path $uygulamaDizini "UDMT.exe"

if (-not (Test-Path $uygulamaYolu)) {
    [System.Windows.MessageBox]::Show(
        "UDMT.exe bulunamadı. Lütfen ZIP dosyasını tamamen çıkardıktan sonra UDMT-Baslat.cmd dosyasını çalıştırın.",
        "UDMT başlatılamadı",
        [System.Windows.MessageBoxButton]::OK,
        [System.Windows.MessageBoxImage]::Error
    ) | Out-Null
    exit 1
}

[xml]$xaml = @"
<Window xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="UDMT — Oyun Seçimi"
        Width="760" Height="540"
        WindowStartupLocation="CenterScreen"
        ResizeMode="NoResize"
        Background="#111827"
        Foreground="#F9FAFB">
    <Grid Margin="34">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
            <RowDefinition Height="Auto"/>
        </Grid.RowDefinitions>

        <StackPanel Grid.Row="0">
            <TextBlock Text="UDMT" FontSize="34" FontWeight="Bold"/>
            <TextBlock Text="Universal Dubbing Modding Tool" Margin="0,4,0,0" FontSize="15" Foreground="#9CA3AF"/>
        </StackPanel>

        <StackPanel Grid.Row="1" Margin="0,28,0,18">
            <TextBlock Text="Çalışmak istediğiniz oyunu seçin" FontSize="22" FontWeight="SemiBold"/>
            <TextBlock Text="Uygulama, seçiminize uygun çalışma alanını otomatik açacaktır." Margin="0,7,0,0" Foreground="#D1D5DB"/>
        </StackPanel>

        <UniformGrid Grid.Row="2" Columns="2" Rows="2">
            <Button x:Name="OutlastButton" Margin="0,0,10,10" Padding="18" HorizontalContentAlignment="Left" Background="#1F2937" Foreground="#F9FAFB" BorderBrush="#374151" BorderThickness="1">
                <StackPanel><TextBlock Text="Outlast" FontSize="20" FontWeight="Bold"/><TextBlock Text="BNK / WEM düzenleme" Margin="0,6,0,0" Foreground="#D1D5DB"/></StackPanel>
            </Button>
            <Button x:Name="WhistleblowerButton" Margin="10,0,0,10" Padding="18" HorizontalContentAlignment="Left" Background="#1F2937" Foreground="#F9FAFB" BorderBrush="#374151" BorderThickness="1">
                <StackPanel><TextBlock Text="Outlast: Whistleblower" FontSize="20" FontWeight="Bold"/><TextBlock Text="BNK / WEM düzenleme" Margin="0,6,0,0" Foreground="#D1D5DB"/></StackPanel>
            </Button>
            <Button x:Name="DmcButton" Margin="0,10,10,0" Padding="18" HorizontalContentAlignment="Left" Background="#1F2937" Foreground="#F9FAFB" BorderBrush="#374151" BorderThickness="1">
                <StackPanel><TextBlock Text="DMC: Devil May Cry" FontSize="20" FontWeight="Bold"/><TextBlock Text="APK arşiv düzenleme" Margin="0,6,0,0" Foreground="#D1D5DB"/></StackPanel>
            </Button>
            <Button x:Name="F1Button" Margin="10,10,0,0" Padding="18" HorizontalContentAlignment="Left" Background="#1F2937" Foreground="#F9FAFB" BorderBrush="#374151" BorderThickness="1">
                <StackPanel><TextBlock Text="F1 25" FontSize="20" FontWeight="Bold"/><TextBlock Text="NeFS arşiv düzenleme" Margin="0,6,0,0" Foreground="#D1D5DB"/></StackPanel>
            </Button>
        </UniformGrid>

        <DockPanel Grid.Row="3" Margin="0,24,0,0">
            <TextBlock DockPanel.Dock="Left" VerticalAlignment="Center" Foreground="#9CA3AF" Text="Önizleme test paketi"/>
            <Button x:Name="CancelButton" DockPanel.Dock="Right" Width="100" Height="36" Content="Çıkış" Background="#374151" Foreground="#F9FAFB" BorderBrush="#4B5563"/>
        </DockPanel>
    </Grid>
</Window>
"@

$okuyucu = New-Object System.Xml.XmlNodeReader $xaml
$pencere = [Windows.Markup.XamlReader]::Load($okuyucu)
$secim = $null

function SecimiTamamla([string]$oyun, [string]$aramaMetni) {
    $script:secim = [PSCustomObject]@{ Oyun = $oyun; AramaMetni = $aramaMetni }
    $pencere.DialogResult = $true
}

$pencere.FindName("OutlastButton").Add_Click({ SecimiTamamla "Outlast" "Wwise" })
$pencere.FindName("WhistleblowerButton").Add_Click({ SecimiTamamla "Outlast: Whistleblower" "Wwise" })
$pencere.FindName("DmcButton").Add_Click({ SecimiTamamla "DMC: Devil May Cry" "APK" })
$pencere.FindName("F1Button").Add_Click({ SecimiTamamla "F1 25" "NeFS" })
$pencere.FindName("CancelButton").Add_Click({ $pencere.DialogResult = $false })

$sonuc = $pencere.ShowDialog()
if ($sonuc -ne $true -or $null -eq $secim) { exit 0 }

$profilYolu = Join-Path $uygulamaDizini "secilen-oyun.txt"
$secim.Oyun | Set-Content -Path $profilYolu -Encoding UTF8

try {
    $islem = Start-Process -FilePath $uygulamaYolu -WorkingDirectory $uygulamaDizini -PassThru
} catch {
    [System.Windows.MessageBox]::Show(
        "UDMT başlatılamadı.`n`n$($_.Exception.Message)`n`nMicrosoft .NET 9 Desktop Runtime x64 kurulu olmalıdır.",
        "UDMT başlatılamadı",
        [System.Windows.MessageBoxButton]::OK,
        [System.Windows.MessageBoxImage]::Error
    ) | Out-Null
    exit 1
}

$sonTarih = (Get-Date).AddSeconds(25)
$kok = $null
while ((Get-Date) -lt $sonTarih -and -not $islem.HasExited) {
    $islem.Refresh()
    if ($islem.MainWindowHandle -ne 0) {
        $kok = [System.Windows.Automation.AutomationElement]::FromHandle($islem.MainWindowHandle)
        if ($null -ne $kok) { break }
    }
    Start-Sleep -Milliseconds 350
}

if ($null -eq $kok) {
    if ($islem.HasExited) {
        [System.Windows.MessageBox]::Show(
            "Uygulama açılırken kapandı. Microsoft .NET 9 Desktop Runtime x64 kurulumunu ve uygulama klasöründeki udmt_error.log dosyasını kontrol edin.",
            "UDMT açılamadı",
            [System.Windows.MessageBoxButton]::OK,
            [System.Windows.MessageBoxImage]::Warning
        ) | Out-Null
    }
    exit 0
}

Start-Sleep -Milliseconds 900
$dugmeKosulu = New-Object System.Windows.Automation.PropertyCondition(
    [System.Windows.Automation.AutomationElement]::ControlTypeProperty,
    [System.Windows.Automation.ControlType]::Button
)
$dugmeler = $kok.FindAll([System.Windows.Automation.TreeScope]::Descendants, $dugmeKosulu)
$hedef = $null
foreach ($dugme in $dugmeler) {
    $ad = $dugme.Current.Name
    if (-not [string]::IsNullOrWhiteSpace($ad) -and $ad.IndexOf($secim.AramaMetni, [System.StringComparison]::OrdinalIgnoreCase) -ge 0) {
        $hedef = $dugme
        break
    }
}

if ($null -ne $hedef) {
    try {
        $desen = $hedef.GetCurrentPattern([System.Windows.Automation.InvokePattern]::Pattern)
        $desen.Invoke()
    } catch {
        [System.Windows.MessageBox]::Show(
            "'$($secim.Oyun)' seçildi ancak ilgili çalışma alanı otomatik açılamadı. Ana ekrandan '$($secim.AramaMetni)' aracını seçebilirsiniz.",
            "Çalışma alanı seçimi",
            [System.Windows.MessageBoxButton]::OK,
            [System.Windows.MessageBoxImage]::Information
        ) | Out-Null
    }
} else {
    [System.Windows.MessageBox]::Show(
        "'$($secim.Oyun)' seçildi. İlgili çalışma alanını ana ekrandan '$($secim.AramaMetni)' başlığıyla açabilirsiniz.",
        "Oyun seçildi",
        [System.Windows.MessageBoxButton]::OK,
        [System.Windows.MessageBoxImage]::Information
    ) | Out-Null
}
