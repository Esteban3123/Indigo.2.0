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
        Me.SPISendElectronicDocuments = New System.ServiceProcess.ServiceProcessInstaller()
        Me.SISendElectronicDocuments = New System.ServiceProcess.ServiceInstaller()
        '
        'SPISendElectronicDocuments
        '
        Me.SPISendElectronicDocuments.Account = System.ServiceProcess.ServiceAccount.LocalService
        Me.SPISendElectronicDocuments.Password = Nothing
        Me.SPISendElectronicDocuments.Username = Nothing
        '
        'SISendElectronicDocuments
        '
        Me.SISendElectronicDocuments.Description = "Servicio de Windows para el envío de Documentos Electrónicos al Adquiriente"
        Me.SISendElectronicDocuments.DisplayName = "Facturas Electrónicas: Envío al Adquiriente"
        Me.SISendElectronicDocuments.ServiceName = "SWSendElectronicDocuments"
        '
        'ProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.SPISendElectronicDocuments, Me.SISendElectronicDocuments})

    End Sub

    Friend WithEvents SPISendElectronicDocuments As ServiceProcess.ServiceProcessInstaller
    Friend WithEvents SISendElectronicDocuments As ServiceProcess.ServiceInstaller
End Class
