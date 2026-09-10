Imports System.Runtime.Serialization

Partial Public Class DefinitionRateDetailCondition

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el nombre de la condicion
    ''' </summary>
    <DataMember()>
    Public Property ConditionName As String

    ''' <summary>
    ''' Obtiene o establece el nombre de la tarifa
    ''' </summary>
    <DataMember()>
    Public Property RateName As String

    ''' <summary>
    ''' Obtiene o establece el nombre de la especialidad 
    ''' de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property SpecialtyDescriptionFirst As String

    ''' <summary>
    ''' Obtiene o establece el nombre de la especialidad 
    ''' de la segunda condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property SpecialtyDescriptionSecond As String

    ''' <summary>
    ''' Obtiene o establece la unidad funcional de la
    ''' primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property FunctionalUnitDescriptionFirst As String

    ''' <summary>
    ''' Obtiene o establece la unidad funcional de la
    ''' segunda condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property FunctionalUnitDescriptionSecond As String

    ''' <summary>
    ''' Obtiene o establece la RIAS de la
    ''' primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property RIASDescriptionFirst As String

    ''' <summary>
    ''' Obtiene o establece la RIAS de la
    ''' segunda condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property RIASDescriptionSecond As String

    ''' <summary>
    ''' Obtiene o establece la Descripción de la
    ''' primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DescriptionCodeNameFirst As String

    ''' <summary>
    ''' Obtiene o establece la Descripción de la
    ''' segunda condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DescriptionCodeNameSecond As String

    ''' <summary>
    ''' Obtiene o establece el nombre de la vigencia del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property RateManualValidityDescription As String

    ''' <summary>
    ''' Obtiene o establece el nombre del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property RateManualDescription As String

    ''' <summary>
    ''' Obtiene o establece el nombre de la primera condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property FirstCondition As String

    ''' <summary>
    ''' Obtiene o establece el nombre de la segunda condicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property SecondCondition As String

    ''' <summary>
    ''' Operador logico de la cabecera
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property LogicOperator As Integer

    ''' <summary>
    ''' retorna el string de tipo de operador
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property FirstOperatorName As String
        Get
            Return Me.GetOperatorName(Me.Operator)
        End Get
    End Property

    ''' <summary>
    ''' retornar el string del tipo de operador 2
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property SecondOperatorName As String
        Get
            Return Me.GetOperatorName(Me.Operator2)
        End Get
    End Property

    Private _liquidationTypeDictionary As Dictionary(Of Byte, String) = New Dictionary(Of Byte, String) From {{1, "Fija"}, {2, "Estándar"}, {3, "Vigencia"}}
    ''' <summary>
    ''' Nombre del tipo de liquidacion
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property LiquidationTypeName As String
        Get
            If _liquidationTypeDictionary.ContainsKey(Me.LiquidationType) Then
                Return _liquidationTypeDictionary(Me.LiquidationType)
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Private _manualTypeDictionary As Dictionary(Of Byte, String) = New Dictionary(Of Byte, String) From {{1, " ISS 2001"}, {2, "ISS 2004"}, {3, "SOAT"}, {4, "Institucional"}}
    ''' <summary>
    ''' nombre del tipo de manual
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ManualTypeName As String
        Get
            If Me.ManualType Is Nothing Then
                Return String.Empty
            End If

            If _manualTypeDictionary.ContainsKey(Me.ManualType) Then
                Return _manualTypeDictionary(Me.ManualType)
            Else
                Return String.Empty
            End If
        End Get
    End Property

    ''' <summary>
    ''' diccionario que tine los tipo de operadores
    ''' </summary>
    Private _operatorDictionary As Dictionary(Of Byte, String) = New Dictionary(Of Byte, String) From {{1, "="}, {2, "<>"}}
#End Region

    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As DefinitionRateDetailCondition
        Dim entity As DefinitionRateDetailCondition = DirectCast(MemberwiseClone(), DefinitionRateDetailCondition)
        Return entity
    End Function

    ''' <summary>
    ''' funcion privada  que retorna el nombre del operador
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Private Function GetOperatorName(value As Byte?) As String
        If value Is Nothing Then
            Return String.Empty
        End If
        If _operatorDictionary.ContainsKey(value) Then
            Return _operatorDictionary(value)
        Else
            Return String.Empty
        End If
    End Function

End Class
