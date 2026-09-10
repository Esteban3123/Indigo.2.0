Imports System.Runtime.Serialization

Partial Public Class DefinitionRateDetail

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el nombre de la regla
    ''' </summary>
    <DataMember()>
    Public Property RuleTypeName As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la regla
    ''' 1-CodigoNombre IPSService
    ''' 2-CodigoNombre CUPS
    ''' 3-CodigoNombre SubGroup
    ''' 4-CodigoNombre Group
    ''' 5-General
    ''' </summary>
    <DataMember()>
    Public Property RuleDescription As String

    ''' <summary>
    ''' Obtiene o establece el nombre de la condición primera
    ''' </summary>
    <DataMember()>
    Public Property ConditionName As String

    ''' <summary>
    ''' Obtiene o establece el nombre de la vigencia del manual tarifario
    ''' </summary>
    <DataMember()>
    Public Property RateManualValidityDescription As String

    ''' <summary>
    ''' Obtiene o establece el nombre del manual tarifario
    ''' </summary>
    <DataMember()>
    Public Property RateManualDescription As String

    ''' <summary>
    ''' Codigo y nombre del servicio IPS
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property IPSServiceCodeName As String

    ''' <summary>
    ''' Codigo y descripcion del CUPS
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CUPSEntityCodeDescription As String

    ''' <summary>
    ''' Codigo y Nombre del Sub grupo del CUPS
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CUPSSubGroupCodeName As String

    ''' <summary>
    ''' Codigo y Nombre del Grupo del CUPS
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CUPSGroupCodeName As String

    ''' <summary>
    ''' variable privada diccionario tipo de condicion
    ''' </summary>
    Private _conditionTypeNameDictionary As Dictionary(Of Byte, String)
    ''' <summary>
    ''' Propiedad que Obtiene los nombre de los tipo de condicion
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ConditionTypeNameDictionary
        Get
            If _conditionTypeNameDictionary Is Nothing Then
                _conditionTypeNameDictionary = New Dictionary(Of Byte, String) From {{1, "Horario"}, {2, "Especialidad"}, {3, "Unidad Funcional"}, {4, "Tipo de Unidad"},
                                                                                     {5, "Ninguna"}, {6, "RIAS"}, {7, "Descripción"}}
            End If
            Return _conditionTypeNameDictionary
        End Get
    End Property

    Private _logicOperatorName As Dictionary(Of Byte, String) = New Dictionary(Of Byte, String) From {{1, ""}, {2, "Y"}, {3, "O"}}

    ''' <summary>
    ''' Nombre del operador Logico
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property LogicOperatorName As String
        Get
            If _logicOperatorName.ContainsKey(Me.LogicalOperator) Then
                Return _logicOperatorName(Me.LogicalOperator)
            Else
                Return String.Empty
            End If
        End Get
    End Property

#End Region
#Region "Private"
    ''' <summary>
    ''' variable privadatupa de los tipo de regla
    ''' </summary>
    Private _ruleTypeTuple As List(Of Tuple(Of Integer, String, String))

    ''' <summary>
    ''' diccionario que almacena los tipos de reglas definicion de tarifa
    ''' </summary>
    Private ReadOnly Property RuleTypeTuple As List(Of Tuple(Of Integer, String, String))
        Get
            If _ruleTypeTuple Is Nothing Then
                _ruleTypeTuple = New List(Of Tuple(Of Integer, String, String)) _
                                    From {New Tuple(Of Integer, String, String)(1, "Servicio IPS", $"{IPSServiceCodeName}"),
                                            New Tuple(Of Integer, String, String)(2, "CUPS", $"{CUPSEntityCodeDescription}"),
                                            New Tuple(Of Integer, String, String)(3, "SubGrupos CUPS", $"{CUPSSubGroupCodeName}"),
                                            New Tuple(Of Integer, String, String)(4, "Grupo CUPS", $"{CUPSGroupCodeName}"),
                                            New Tuple(Of Integer, String, String)(5, "General", "")}
            Else
                _ruleTypeTuple(0) = New Tuple(Of Integer, String, String)(1, "Servicio IPS", $"{IPSServiceCodeName}")
                _ruleTypeTuple(1) = New Tuple(Of Integer, String, String)(2, "CUPS", $"{CUPSEntityCodeDescription}")
                _ruleTypeTuple(2) = New Tuple(Of Integer, String, String)(3, "SubGrupos CUPS", $"{CUPSSubGroupCodeName}")
                _ruleTypeTuple(3) = New Tuple(Of Integer, String, String)(4, "Grupo CUPS", $"{CUPSGroupCodeName}")
                _ruleTypeTuple(4) = New Tuple(Of Integer, String, String)(5, "General", "")
            End If
            Return _ruleTypeTuple
        End Get
    End Property
#End Region

    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As DefinitionRateDetail
        Dim entity As DefinitionRateDetail = DirectCast(MemberwiseClone(), DefinitionRateDetail)
        Return entity
    End Function

    ''' <summary>
    ''' retorna el nombre del tipo de regla
    ''' </summary>
    ''' <returns></returns>
    Public Function GetRuleTypeName() As String
        Return RuleTypeTuple.Find(Function(x) x.Item1 = Me.RuleType).Item2
    End Function

    ''' <summary>
    ''' retorna la descripcion de la regla
    ''' </summary>
    ''' <returns></returns>
    Public Function GetRuleDescription() As String
        Return RuleTypeTuple.Find(Function(x) x.Item1 = Me.RuleType).Item3
    End Function

    ''' <summary>
    ''' retorna el codigo y nombre del tipo de condicion actual
    ''' </summary>
    ''' <returns></returns>
    Public Function GetConditionTypeName() As String
        Dim name = $"{Me.ConditionTypeNameDictionary(Me.ConditionType)}"
        If Me.LogicalOperator > 1 Then
            name &= $" {If(Me.LogicalOperator = 2, "Y", "O")} {Me.ConditionTypeNameDictionary(Me.ConditionType2)}"
        End If
        Return name
    End Function

End Class
