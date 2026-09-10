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

Public Class ProductionLineRepository
    Inherits GenericRepository(Of ProductionLine)
    Implements IProductionLineRepository, Inject
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
    ''' <summary>
    ''' Lista todos los tipos de dosis unitaria
    ''' </summary>
    ''' <returns>Lista de tipos de dosis unitaria</returns>
    ''' <remarks></remarks>
    Public Function ListAllProductionLine() As List(Of ProductionLine) Implements IProductionLineRepository.ListAllProductionLine
        Dim productionLine = From e In _context.ProductionLine
                             Select e
        Return productionLine.ToList()
    End Function
    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria especifica
    ''' </summary>
    ''' <param name="code">Codigo del tipo de dosis unitaria</param>
    ''' <returns>Tipo de Dosis Unitaria</returns>
    ''' <remarks></remarks>
    Public Function GetProductionLine(code As String, Optional tracking As Boolean = True) As ProductionLine Implements IProductionLineRepository.GetProductionLine
        Dim productionLine As ProductionLine = Nothing
        If tracking Then
            productionLine = (From e In _context.ProductionLine.Include("ProductionLineUnitDoseType").Include("ProductionLineSchedule").Include("ProductionLineScheduleException")
                              Where e.Code = code
                              Select e).FirstOrDefault
        Else
            productionLine = (From e In _context.ProductionLine.AsNoTracking.Include("ProductionLineUnitDoseType").AsNoTracking.Include("ProductionLineSchedule").AsNoTracking.Include("ProductionLineScheduleException").AsNoTracking
                              Where e.Code = code
                              Select e).FirstOrDefault
        End If
        If productionLine IsNot Nothing Then
            Dim _DayName As String = String.Empty
            If productionLine.ProductionLineSchedule IsNot Nothing AndAlso productionLine.ProductionLineSchedule.Count > 0 Then
                For Each s In productionLine.ProductionLineSchedule
                    Select Case s.DayId
                        Case 1
                            _DayName = "Lunes"
                        Case 2
                            _DayName = "Martes"
                        Case 3
                            _DayName = "Miércoles"
                        Case 4
                            _DayName = "Jueves"
                        Case 5
                            _DayName = "Viernes"
                        Case 6
                            _DayName = "Sábado"
                        Case 7
                            _DayName = "Domingo"
                        Case 8
                            _DayName = "Festivo"
                    End Select

                    s.DayName = _DayName
                Next
            End If

            If productionLine.ProductionLineUnitDoseType IsNot Nothing AndAlso productionLine.ProductionLineUnitDoseType.Count > 0 Then
                For Each item In productionLine.ProductionLineUnitDoseType
                    Dim unitDoseType = (From x In _context.UnitDoseType.AsNoTracking Where x.Id = item.Id_UnitDoseType Select x).FirstOrDefault()
                    item.UnitDoseTypeCode = unitDoseType.Code
                    item.UnitDoseTypeName = unitDoseType.Description
                    item.MsClass = unitDoseType.MSClass
                Next
            End If

            Return productionLine
        Else
            Return New ProductionLine()
        End If
    End Function
    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por el identificador
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetProductionLineId(id As String, Optional tracking As Boolean = True) As ProductionLine Implements IProductionLineRepository.GetProductionLineId
        Dim productionLine = From e In _context.ProductionLine
                             Where e.Id = id
                             Select e
        If productionLine.Count > 0 Then
            Dim ObjProductionLine = Nothing
            If tracking = False Then
                ObjProductionLine = (From e In _context.ProductionLine.AsNoTracking
                                     Where e.Id = id
                                     Select e).SingleOrDefault
            Else
                ObjProductionLine = productionLine.SingleOrDefault
            End If
            Return ObjProductionLine
        Else
            Return New ProductionLine()
        End If
    End Function
End Class
