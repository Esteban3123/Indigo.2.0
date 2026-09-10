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
    Private WithEvents IndexerServiceProcessInstaller As System.ServiceProcess.ServiceProcessInstaller
    Private WithEvents IndexerServiceInstaller As System.ServiceProcess.ServiceInstaller

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.IndexerServiceProcessInstaller = New System.ServiceProcess.ServiceProcessInstaller()
        Me.IndexerServiceInstaller = New System.ServiceProcess.ServiceInstaller()
        '
        'IndexerServiceProcessInstaller
        '
        Me.IndexerServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem
        Me.IndexerServiceProcessInstaller.Password = Nothing
        Me.IndexerServiceProcessInstaller.Username = Nothing
        '
        'IndexerServiceInstaller
        '
        Me.IndexerServiceInstaller.Description = "Agente encargado de la indexación de documentos almacenados en la cola de mensajes MSMQ"
        Me.IndexerServiceInstaller.DisplayName = "Indigo Agente de Indexación"
        Me.IndexerServiceInstaller.ServiceName = "IndexerAgent"
        Me.IndexerServiceInstaller.StartType = ServiceProcess.ServiceStartMode.Automatic
        '
        'ProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.IndexerServiceInstaller, Me.IndexerServiceProcessInstaller})
    End Sub

End Class
