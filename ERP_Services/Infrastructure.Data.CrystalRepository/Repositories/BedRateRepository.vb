'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Diego Andrés Roldán
' Created          : 23-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities

Public Class BedRateRepository
    Inherits GenericRepository(Of CHGENTARI)
    Implements IBedRateRepository

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

    ''' <summary>
    ''' obtiene una tariga de cama por codigo
    ''' </summary>
    ''' <returns></returns>
    Public Function GetBedRatebyBedCode(bedCode As Integer) As List(Of CHGENTARI) Implements IBedRateRepository.GetBedRatebyBedCode
        Dim res = (From b In _crystalContext.CHGENTARI.Include("ADCENATEN").AsNoTracking().Include("INUNIFUNC").AsNoTracking().Include("CHTIPESTA").AsNoTracking().Include("CHCAMASHO").AsNoTracking() Where b.CHCAMASHO.CODICAMAS = bedCode Select b).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then

            For Each item In res
                Dim careCenter = (From c In _crystalContext.ADCENATEN.AsNoTracking() Where c.CODCENATE = item.ADCENATEN.CODCENATE Select New With {.CODCENATE1 = c.CODCENATE, .NOMCENATE1 = c.NOMCENATE}).FirstOrDefault()
                Dim funcUnit = (From c In _crystalContext.INUNIFUNC.AsNoTracking() Where c.UFUCODIGO = item.INUNIFUNC.UFUCODIGO Select New With {.UFUCODIGO1 = c.UFUCODIGO, .UFUDESCRI1 = c.UFUDESCRI}).FirstOrDefault()
                Dim satyType = (From c In _crystalContext.CHTIPESTA.AsNoTracking() Where c.CODTIPEST = item.CHTIPESTA.CODTIPEST Select New With {.CODTIPEST1 = c.CODTIPEST, .DESTIPEST1 = c.DESTIPEST}).FirstOrDefault()
                item.CareCenterFullName = String.Concat(careCenter.CODCENATE1.Trim(), " - ", careCenter.NOMCENATE1.Trim())
                item.FunctionalUnitFullName = String.Concat(funcUnit.UFUCODIGO1.Trim(), " - ", funcUnit.UFUDESCRI1.Trim())
                item.StayTypeFullName = String.Concat(satyType.CODTIPEST1.Trim(), " - ", satyType.DESTIPEST1.Trim())
            Next

            Return res
        Else
            Return Nothing
        End If
    End Function
    
    ''' <summary>
    ''' obtiene una tarifa de cama por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetBedRatebyCode(code As Integer) As CHGENTARI Implements IBedRateRepository.GetBedRatebyCode
        Dim res = (From b In _crystalContext.CHGENTARI Where b.CODCONCEC = code Select b).FirstOrDefault()
        If res IsNot Nothing AndAlso res.CODCONCEC > 0 Then

            Dim careCenter = (From c In _crystalContext.ADCENATEN.AsNoTracking() Where c.CODCENATE = res.ADCENATEN.CODCENATE Select New With {.CODCENATE1 = c.CODCENATE, .NOMCENATE1 = c.NOMCENATE}).FirstOrDefault()
            Dim funcUnit = (From c In _crystalContext.INUNIFUNC.AsNoTracking() Where c.UFUCODIGO = res.INUNIFUNC.UFUCODIGO Select New With {.UFUCODIGO1 = c.UFUCODIGO, .UFUDESCRI1 = c.UFUDESCRI}).FirstOrDefault()
            Dim satyType = (From c In _crystalContext.CHTIPESTA.AsNoTracking() Where c.CODTIPEST = res.CHTIPESTA.CODTIPEST Select New With {.CODTIPEST1 = c.CODTIPEST, .DESTIPEST1 = c.DESTIPEST}).FirstOrDefault()
            res.CareCenterFullName = String.Concat(careCenter.CODCENATE1.Trim(), " - ", careCenter.NOMCENATE1.Trim())
            res.FunctionalUnitFullName = String.Concat(funcUnit.UFUCODIGO1.Trim(), " - ", funcUnit.UFUDESCRI1.Trim())
            res.StayTypeFullName = String.Concat(satyType.CODTIPEST1.Trim(), " - ", satyType.DESTIPEST1.Trim())

            Return res
        Else
            Return Nothing
        End If
    End Function

End Class