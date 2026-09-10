Import-Module -Name ServerManager
Install-WindowsFeature -Name Web-Server -IncludeManagementTools
Import-Module -Name WebAdministration
Get-PSProvider -PSProvider WebAdministration
Get-WebBinding -name "Default Web Site"
$Cert = New-SelfSignedCertificate -dnsName "<Server FQDN>" `
                                  -CertStoreLocation cert:\LocalMachine\My `
                                  -KeyLength 2048 `
                                  -NotAfter (Get-Date).AddYears(2)
New-WebBinding -Name "Default Web Site" -protocol https -port 443
$Cert | New-Item -path IIS:\SslBindings\0.0.0.0!443