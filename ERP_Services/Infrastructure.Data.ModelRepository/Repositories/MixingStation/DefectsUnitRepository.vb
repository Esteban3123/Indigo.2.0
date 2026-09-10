'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 29/08/2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Class DefectsUnitRepository

    Inherits GenericRepository(Of DefectsUnitDoseType)
    Implements IDefectsUnitRepository, Inject
    ''' <summary>
    ''' Contexto de Tipo de dosis unitaria
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork
    ''' <summary>
    ''' Inicia el contexto de Tipo de Dosis Unitaria
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function ListAllDefectsUnitDoseType(Id_DefectsClassificationItem As Integer) As List(Of Tuple(Of Integer, String)) Implements IDefectsUnitRepository.ListAllDefectsUnitDoseType
        Dim Lista As New List(Of Tuple(Of Integer, String))

        Dim query = (From e In _context.DefectsUnitDoseType
                     Join u In _context.UnitDoseType
                                      On e.Id_UnitDoseType Equals u.Id
                     Where e.Id_DefectsClassificationItem = Id_DefectsClassificationItem
                     Select New With {e.Id_UnitDoseType, u.Description}).ToList()

        For Each item In query
            Lista.Add(New Tuple(Of Integer, String)(item.Id_UnitDoseType, item.Description))
        Next
        Return Lista
    End Function
End Class
