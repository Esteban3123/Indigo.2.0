'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Ruben Dario Castañeda Giraldo
' Created          : 21/05/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

Public Class ProductionLineUnitRepository
    Inherits GenericRepository(Of ProductionLineUnitDoseType)
    Implements IProductionLineUnitRepository, Inject
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

    Public Function ListAllProductionLineUnitDoseType(Id_ProductionLine As Integer) As List(Of Tuple(Of Integer, String)) Implements IProductionLineUnitRepository.ListAllProductionLineUnitDoseType

        Dim Lista As New List(Of Tuple(Of Integer, String))

        Dim query = (From e In _context.ProductionLineUnitDoseType
                     Join u In _context.UnitDoseType
                                      On e.Id_UnitDoseType Equals u.Id
                     Where e.Id_ProductionLine = Id_ProductionLine
                     Select New With {e.Id_UnitDoseType, u.Description}).ToList()

        For Each item In query
            Lista.Add(New Tuple(Of Integer, String)(item.Id_UnitDoseType, item.Description))
        Next
        Return Lista
    End Function

End Class
