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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.SPIElectronicSupportDocument = New System.ServiceProcess.ServiceProcessInstaller()
        Me.SIElectronicSupportDocument = New System.ServiceProcess.ServiceInstaller()
        '
        'SPIElectronicSupportDocument
        '
        Me.SPIElectronicSupportDocument.Account = System.ServiceProcess.ServiceAccount.LocalService
        Me.SPIElectronicSupportDocument.Password = Nothing
        Me.SPIElectronicSupportDocument.Username = Nothing
        '
        'SIElectronicSupportDocument
        '
        Me.SIElectronicSupportDocument.Description = "Servicio windows para generación y envío de documentos de soporte electrónicos a " &
    "la DIAN"
        Me.SIElectronicSupportDocument.DisplayName = "Documento Soporte Electrónico: Envío a la DIAN"
        Me.SIElectronicSupportDocument.ServiceName = "SWElectronicSupportDocument"
        '
        'ProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.SPIElectronicSupportDocument, Me.SIElectronicSupportDocument})

    End Sub

    Friend WithEvents SPIElectronicSupportDocument As ServiceProcess.ServiceProcessInstaller
    Friend WithEvents SIElectronicSupportDocument As ServiceProcess.ServiceInstaller
End Class
