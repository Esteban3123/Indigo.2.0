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
        Me.SPIValidateElectronicDocuments = New System.ServiceProcess.ServiceProcessInstaller()
        Me.SIValidateElectronicDocuments = New System.ServiceProcess.ServiceInstaller()
        '
        'SPIValidateElectronicDocuments
        '
        Me.SPIValidateElectronicDocuments.Account = System.ServiceProcess.ServiceAccount.LocalService
        Me.SPIValidateElectronicDocuments.Password = Nothing
        Me.SPIValidateElectronicDocuments.Username = Nothing
        '
        'SIValidateElectronicDocuments
        '
        Me.SIValidateElectronicDocuments.Description = "Servicio de Windows para la validación de Documentos Electrónicos enviados a la D"& _ 
    "IAN"
        Me.SIValidateElectronicDocuments.DisplayName = "Facturas Electrónicas: Validar en la DIAN"
        Me.SIValidateElectronicDocuments.ServiceName = "SWValidateElectronicDocuments"
        Me.SIValidateElectronicDocuments.StartType = System.ServiceProcess.ServiceStartMode.Automatic
        '
        'ProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.SPIValidateElectronicDocuments, Me.SIValidateElectronicDocuments})

End Sub

    Friend WithEvents SPIValidateElectronicDocuments As ServiceProcess.ServiceProcessInstaller
    Friend WithEvents SIValidateElectronicDocuments As ServiceProcess.ServiceInstaller
End Class
