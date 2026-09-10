<System.ComponentModel.RunInstaller(True)> Partial Class ProjectInstaller
    Inherits System.Configuration.Install.Installer

    'Installer overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.SPIElectronicDocuments = New System.ServiceProcess.ServiceProcessInstaller()
        Me.SIElectronicDocuments = New System.ServiceProcess.ServiceInstaller()
        '
        'SPIElectronicDocuments
        '
        Me.SPIElectronicDocuments.Account = System.ServiceProcess.ServiceAccount.LocalService
        Me.SPIElectronicDocuments.Password = Nothing
        Me.SPIElectronicDocuments.Username = Nothing
        '
        'SIElectronicDocuments
        '
        Me.SIElectronicDocuments.Description = "Servicio de Windows para la generación y envío de Documentos Electrónicos a la DI"& _ 
    "AN"
        Me.SIElectronicDocuments.DisplayName = "Facturas Electrónicas: Envío a la DIAN"
        Me.SIElectronicDocuments.ServiceName = "SWElectronicDocuments"
        '
        'ProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.SPIElectronicDocuments, Me.SIElectronicDocuments})

End Sub

    Friend WithEvents SPIElectronicDocuments As ServiceProcess.ServiceProcessInstaller
    Friend WithEvents SIElectronicDocuments As ServiceProcess.ServiceInstaller
End Class
