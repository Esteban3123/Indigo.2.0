'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Diego Andrés Roldán
' Created          : 24-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities

Public Class AGACTMDDDRepository
    Inherits GenericRepository(Of AGACTMDDD)
    Implements IAGACTMDDDRepository

    'Contexto del repositorio de Indigo Vie Cloud Platform
    Private _crystalContext As ICrystalModelUnitOfWork

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="crystalContext">Contexto</param>
    Public Sub New(ByVal crystalContext As ICrystalModelUnitOfWork)
        MyBase.New(crystalContext)
        Me._crystalContext = crystalContext
    End Sub

    Public Function GetAGACTMDDDByCODACTMED(codactmed As String) As List(Of AGACTMDDD) Implements IAGACTMDDDRepository.GetAGACTMDDDByCODACTMED
        Return (From b In _crystalContext.AGACTMDDD Where b.CODACTMED = codactmed Select b).ToList()
    End Function

    Public Function GetAGACTMDDDByCODACTMEDProduct(codactmed As String) As List(Of AGACTMDDD) Implements IAGACTMDDDRepository.GetAGACTMDDDByCODACTMEDProduct
        Dim p As String = "P"
        Return (From b In _crystalContext.AGACTMDDD Where b.CODACTMED = codactmed AndAlso b.TIPOPARAM = p Select b).ToList()
    End Function

    Public Function GetAGACTMDDDByCODACTMEDListAndCupsCodeList(listActmedicaCups As List(Of ACTMEDCUPS), IPFECNACI As Date) As List(Of AGACTMDDD) Implements IAGACTMDDDRepository.GetAGACTMDDDByCODACTMEDListAndCupsCodeList
        Dim p As String = "P"
        Dim edad As Integer = Math.Abs(Date.Now.Year - IPFECNACI.Year)
        Dim listReturn As New List(Of AGACTMDDD)()

        'If listActmedicaCups IsNot Nothing AndAlso listActmedicaCups.Where(Function(x) x.CODACTMED Is Nothing).Count > 0 Then
        '    For Each i In listActmedicaCups
        '        Dim l2 As AGACTIMED = (From b In _crystalContext.AGACTIMED Where b.CODSERIPS.Equals(i.CODCUPS) Select b).FirstOrDefault()
        '        If l2 IsNot Nothing Then
        '            i.CODACTMED = l2.CODACTMED
        '        End If
        '    Next
        'End If

        For Each i In listActmedicaCups
            If String.IsNullOrEmpty(i.CODACTMED) Then
                Dim l2 As AGACTIMED = (From b In _crystalContext.AGACTIMED Where b.CODSERIPS.Equals(i.CODCUPS) Select b).FirstOrDefault()
                If l2 IsNot Nothing Then
                    i.CODACTMED = l2.CODACTMED
                End If
            End If
            If Not String.IsNullOrEmpty(i.CODACTMED) Then
                Dim l As List(Of AGACTMDDD) = (From b In _crystalContext.AGACTMDDD Where b.CODACTMED.Equals(i.CODACTMED) AndAlso b.CODSERIPS.Equals(i.CODCUPS) AndAlso b.TIPOPARAM = p Select b).ToList()
                If l IsNot Nothing AndAlso l.Count > 0 Then
                    If l.Any(Function(o) (o.APLRANEDA IsNot Nothing AndAlso o.APLRANEDA AndAlso o.EDADESDE <= edad AndAlso o.EDAHASTA >= edad) OrElse (o.APLRANEDA Is Nothing OrElse Not o.APLRANEDA)) Then
                        listReturn.AddRange(l.Where(Function(o) (o.APLRANEDA IsNot Nothing AndAlso o.APLRANEDA AndAlso o.EDADESDE <= edad AndAlso o.EDAHASTA >= edad) OrElse (o.APLRANEDA Is Nothing OrElse Not o.APLRANEDA)).ToList())
                    End If
                End If
            End If
        Next

        'Dim l As List(Of AGACTMDDD) = (From b In _crystalContext.AGACTMDDD Where listActmedicaCups.Where(Function(x) x.CODACTMED IsNot Nothing).Select(Function(x) x.CODACTMED).ToList().Contains(b.CODACTMED) AndAlso b.TIPOPARAM = p Select b).ToList()
        'If l IsNot Nothing AndAlso l.Count > 0 Then
        '    For Each i In l
        '        If (i.APLRANEDA IsNot Nothing AndAlso i.APLRANEDA AndAlso i.EDADESDE <= edad AndAlso i.EDAHASTA >= edad) OrElse (i.APLRANEDA Is Nothing OrElse Not i.APLRANEDA) Then
        '            listReturn.Add(i)
        '        End If
        '    Next
        'End If
        Return listReturn
    End Function
End Class