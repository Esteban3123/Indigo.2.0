Imports System.Runtime.Serialization

Partial Public Class CareGroupRate

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el id de la plantilla de procedimientos
    ''' </summary>
    <DataMember()>
    Public Property ProceduresTemplateId As Integer

    ''' <summary>
    ''' Obtiene o establece el id del subGrupo
    ''' </summary>
    <DataMember()>
    Public Property SubGroupId As Integer

    ''' <summary>
    ''' Obtiene o establece el id del grupo
    ''' </summary>
    <DataMember()>
    Public Property GroupId As Integer

    ''' <summary>
    ''' Obtiene o establece el id de la unidad de mercadeo
    ''' </summary>
    <DataMember()>
    Public Property MarketingUnitId As Integer

    ''' <summary>
    ''' Obtiene o establece el codigo y el nombre de la unidad de mercadeo
    ''' </summary>
    <DataMember()>
    Public Property CodeNameMarketingUnit As String

    ''' <summary>
    ''' Obtiene o establece el codigo y el nombre del grupo
    ''' </summary>
    <DataMember()>
    Public Property CodeNameGroup As String

    ''' <summary>
    ''' Obtiene o establece el codigo y el nombre del subGrupo
    ''' </summary>
    <DataMember()>
    Public Property CodeNameSubGroup As String

    ''' <summary>
    ''' Obtiene o establece el codigo y el nombre de la entidad CUPS
    ''' </summary>
    <DataMember()>
    Public Property CodeNameCUPS As String

    ''' <summary>
    ''' Obtiene o establece si el item fue seleccionado en la rejilla con los check
    ''' </summary>
    <DataMember()>
    Public Property SelectOption As Boolean

    ''' <summary>
    ''' Obtiene o establece el nombre del tipo de liquidacion
    ''' </summary>
    <DataMember()>
    Public Property LiquidationTypeName As String

#End Region

#Region "Clone"

    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As CareGroupRate
        Dim entity As CareGroupRate = DirectCast(MemberwiseClone(), CareGroupRate)
        Return entity
    End Function

#End Region

End Class
