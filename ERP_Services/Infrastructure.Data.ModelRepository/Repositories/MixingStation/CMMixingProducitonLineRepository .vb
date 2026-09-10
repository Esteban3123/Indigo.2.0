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

Public Class CMMixingProducitonLineRepository
    Inherits GenericRepository(Of CMMixingProducitonLine)
    Implements ICMMixingProducitonLineRepository, Inject
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

    Public Function ListAllCMMixingProducitonLine(Id_MixingStation As Integer) As List(Of Tuple(Of Integer, String)) Implements ICMMixingProducitonLineRepository.ListAllCMMixingProducitonLine

        Dim Lista As New List(Of Tuple(Of Integer, String))

        Dim query = (From e In _context.CMMixingProducitonLine
                     Join u In _context.ProductionLine
                                      On e.Id_ProductionLine Equals u.Id
                     Where e.Id_CMConfiguration = Id_MixingStation
                     Select New With {e.Id_ProductionLine, u.Name}).ToList()

        For Each item In query
            Lista.Add(New Tuple(Of Integer, String)(item.Id_ProductionLine, item.Name))
        Next
        Return Lista
    End Function

End Class
