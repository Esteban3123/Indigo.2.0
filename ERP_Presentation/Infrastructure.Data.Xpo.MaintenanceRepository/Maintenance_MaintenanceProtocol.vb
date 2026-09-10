Imports DevExpress.Xpo

Partial Public Class Maintenance_MaintenanceProtocol

#Region "Custom Members"

    <PersistentAlias("FixedAssetItemId.CodeDescription")>
    Public ReadOnly Property FixedAssetItemName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("FixedAssetItemName"))
        End Get
    End Property

    <PersistentAlias("Iif(Type = 1, 'Pruebas de seguridad', Iif(Type = 2, 'Verificación y Calibración', Iif(Type = 3, 'Matenimiento Preventivo', Iif(Type = 4, 'Mantenimiento Correctivo', Iif(Type = 5, 'Otros', '')))))")>
    Public ReadOnly Property TypeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("TypeName"))
        End Get
    End Property

    <PersistentAlias("concat(ResponsibleTypeId.Code, ' - ', ResponsibleTypeId.Description)")>
    Public ReadOnly Property ResponsibleTypeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ResponsibleTypeName"))
        End Get
    End Property

    <PersistentAlias("Iif(State = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StateName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StateName"))
        End Get
    End Property

    <PersistentAlias("Concat(Code, ' - ', Name)")>
    Public ReadOnly Property CodeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class