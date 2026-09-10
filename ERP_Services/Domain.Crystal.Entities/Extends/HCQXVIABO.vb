Imports System.Runtime.Serialization

Partial Public Class HCQXVIABO

    Private _homologationSurgicalInterventionType As List(Of Tuple(Of Byte, String, Byte))
    <DataMember()>
    Private ReadOnly Property HomologationSurgicalInterventionType As List(Of Tuple(Of Byte, String, Byte))
        Get
            If _homologationSurgicalInterventionType Is Nothing Then
                _homologationSurgicalInterventionType = New List(Of Tuple(Of Byte, String, Byte))
                _homologationSurgicalInterventionType.Add(New Tuple(Of Byte, String, Byte)(1, "Basico", 1))
                _homologationSurgicalInterventionType.Add(New Tuple(Of Byte, String, Byte)(2, "Bilateral", 2))
                _homologationSurgicalInterventionType.Add(New Tuple(Of Byte, String, Byte)(3, "Bilateral Multiple", 2))
                _homologationSurgicalInterventionType.Add(New Tuple(Of Byte, String, Byte)(4, "MIVIE (Multiple Igual Via Igual Especialista)", 3))
                _homologationSurgicalInterventionType.Add(New Tuple(Of Byte, String, Byte)(5, "MDVIE (Multiple Diferente Via Igual Especialista)", 4))
                _homologationSurgicalInterventionType.Add(New Tuple(Of Byte, String, Byte)(6, "MIVDE (Multiple Igual Via Diferente Especialista)", 5))
                _homologationSurgicalInterventionType.Add(New Tuple(Of Byte, String, Byte)(7, "MDVDE (Multiple Diferente Via Diferente Especialista)", 6))
                _homologationSurgicalInterventionType.Add(New Tuple(Of Byte, String, Byte)(8, "PolitraumaIV (Politrauma Igual Via)", 7))
                _homologationSurgicalInterventionType.Add(New Tuple(Of Byte, String, Byte)(9, "PolitraumaDV (Politrauma Diferente Via)", 8))
                _homologationSurgicalInterventionType.Add(New Tuple(Of Byte, String, Byte)(10, "No Cruento", 9))
            End If

            Return _homologationSurgicalInterventionType
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que traduce los tipo de via de abordaje en los tipos de intervenciones qx de service order detail
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public ReadOnly Property SurgicalInterventionType As Byte?
        Get
            Return HomologationSurgicalInterventionType?.Find(Function(x) x.Item1 = Me.GENCODVIA)?.Item3
        End Get
    End Property
End Class
