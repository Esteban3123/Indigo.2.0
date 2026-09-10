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
        Me.AuditServiceProcessInstaller = New System.ServiceProcess.ServiceProcessInstaller()
        Me.AuditServiceInstaller = New System.ServiceProcess.ServiceInstaller()
        '
        'AuditServiceProcessInstaller
        '
        Me.AuditServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem
        Me.AuditServiceProcessInstaller.Password = Nothing
        Me.AuditServiceProcessInstaller.Username = Nothing
        '
        'AuditServiceInstaller
        '
        Me.AuditServiceInstaller.Description = "Este Servicio es Vital para Completar el Proceso de Auditoria en el Sistema. Es e" & _
    "l Encargado de Leer de la Cola de MSMQ los Mensajes de Auditoria y Grabarlos en " & _
    "el Repositorio de Datos"
        Me.AuditServiceInstaller.DisplayName = "Indigo Audit Service"
        Me.AuditServiceInstaller.ServiceName = "IndigoAuditService"
        Me.AuditServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic
        '
        'ProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.AuditServiceInstaller, Me.AuditServiceProcessInstaller})

    End Sub
    Friend WithEvents AuditServiceProcessInstaller As System.ServiceProcess.ServiceProcessInstaller
    Friend WithEvents AuditServiceInstaller As System.ServiceProcess.ServiceInstaller

End Class
