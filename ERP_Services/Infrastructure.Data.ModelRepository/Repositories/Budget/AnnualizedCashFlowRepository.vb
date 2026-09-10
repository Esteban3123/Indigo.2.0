'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Juan Carlos Bermudez
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class AnnualizedCashFlowRepository
    Inherits GenericRepository(Of AnnualizedCashFlow)
    Implements IAnnualizedCashFlowRepository

    'Contexto de Budget
    Private _context As IGlobalModelUnitOfWork

#Region "Construct"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtener los rubros del PAC inicial 
    ''' </summary>
    ''' <param name="ValidityId">Id de la vigencia</param>
    ''' <param name="categoryCode">codigo del rubro</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAnnualizedCashFlowByValidityIdAndByCodeCategory(ValidityId As Integer, categoryCode As String, type As Integer) As ActionResult(Of List(Of AnnualizedCashFlow)) Implements IAnnualizedCashFlowRepository.GetAnnualizedCashFlowByValidityIdAndByCodeCategory
        Dim category = (From c In _context.Category Where c.Code = categoryCode And c.BudgetaryValidityId = ValidityId And c.ItemType = type Select c).FirstOrDefault()
        Dim listAnnualizedCashFlow As List(Of AnnualizedCashFlow)
        If category IsNot Nothing Then
            If category.PAC Then
                ' consultamos la fuente de financianción para agregar el codigo y el nombre a la propiedad extendida
                Dim financialSource = (From fs In _context.FinancialSource.AsNoTracking() Where fs.Id = category.FinancialSourceId Select fs).FirstOrDefault()
                Dim FinancialSourceDescription = financialSource.Code + " - " + financialSource.Name


                'consultamos los presupuestos iniciales para calcular el valor presupuestado del rubro
                Dim consulta = (From b In _context.Budget Where b.CategoryId = category.Id Select b.TotalBudget).ToList()
                Dim TotalBudgetValue = IIf(consulta.Count > 0, consulta.Sum, 0)

                ' consultamos los pac del rubro
                listAnnualizedCashFlow = (From e In _context.AnnualizedCashFlow.Include("Category") Where e.CategoryId = category.Id Select e).ToList()
                'si el listado de pac es diferente de vacio le agregamos la entidad del rubro, si el listado es vacio armamos un nuevo listado.
                If Not (listAnnualizedCashFlow IsNot Nothing AndAlso listAnnualizedCashFlow.Count > 0) Then
                    listAnnualizedCashFlow = New List(Of AnnualizedCashFlow)
                    For i As Integer = 1 To 12
                        Dim annualizedcashFlow As New AnnualizedCashFlow
                        With annualizedcashFlow
                            .CategoryId = category.Id
                            .Category = category
                            .Month = i
                            .InitialValue = 0
                            .DebitModificationValue = 0
                            .CreditModificationValue = 0
                            .DebitTransferValue = 0
                            .CreditTransferValue = 0
                            .TotalScheduled = 0
                            .ExecutedValue = 0
                            .Balance = 0
                            .ReserveValue = 0
                            .DebitReserveModValue = 0
                            .CreditReserveModValie = 0
                            .DebitReserveTransValue = 0
                            .CreditReserveTransValue = 0
                            .ExecutedReserveValue = 0
                            .CxPValue = 0
                            .DebitCxPModificationValue = 0
                            .CreditCxPModificationValue = 0
                            .DebitCxPTransferValue = 0
                            .CreditCxPTransferValue = 0
                            .ExecutedCxPValue = 0
                            .Status = 1
                            listAnnualizedCashFlow.Add(annualizedcashFlow)
                        End With
                    Next
                End If
                Dim listString As New List(Of String)
                listString.Add(FinancialSourceDescription)
                listString.Add(TotalBudgetValue.ToString)
                Return New ActionResult(Of List(Of AnnualizedCashFlow)) With {.StateResult = True, .ObjectEmbbeded = listAnnualizedCashFlow, .MessageResult = listString}
            Else
                Return New ActionResult(Of List(Of AnnualizedCashFlow)) With {.StateResult = False, .Message = "El rubro no maneja control PAC"}
            End If
        Else
            Return New ActionResult(Of List(Of AnnualizedCashFlow)) With {.StateResult = False, .Message = "El rubro No existe"}
        End If
    End Function


    ''' <summary>
    ''' Obtener todos los registros de PAC inicial por vigencia 
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <returns></returns>
    Public Function GetAnnualizedCashFlowByValidityId(ValidityId As Integer) As List(Of AnnualizedCashFlow) Implements IAnnualizedCashFlowRepository.GetAnnualizedCashFlowByValidityId
        Dim res = (From acf In _context.AnnualizedCashFlow Join
                   c In _context.Category On acf.CategoryId Equals c.Id Where c.BudgetaryValidityId = ValidityId Select acf).ToList()
        For Each item In res
            Dim category = (From c In _context.Category.AsNoTracking() Where c.Id = item.CategoryId Select c).FirstOrDefault()
            item.CodeNameCategory = String.Concat(category.Code, " - ", category.Name)
            If category.FinancialSourceId IsNot Nothing Then
                item.CategoryResource = (From fs In _context.FinancialSource.AsNoTracking() Where fs.Id = category.FinancialSourceId Select String.Concat(fs.Code, " - ", fs.Name)).FirstOrDefault()
            End If
        Next
        Return res
    End Function

    ''' <summary>
    ''' Obtener todos los registros de PAC inicial por vigencia 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetAnnualizedCashFlowById(id As Integer) As AnnualizedCashFlow Implements IAnnualizedCashFlowRepository.GetAnnualizedCashFlowById
        Dim resul = (From ac In _context.AnnualizedCashFlow.AsNoTracking Where ac.Id = id Select ac).FirstOrDefault
        If resul IsNot Nothing Then
            Return resul
        Else
            Return New AnnualizedCashFlow
        End If
    End Function

    ''' <summary>
    ''' Obtiene un registro del pac inicial por id del rubro y el mes
    ''' </summary>
    ''' <param name="categoryId"></param>
    ''' <param name="month"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAnnualizedCashFlowByCategoryIdAndByMonth(categoryId As Integer, month As Integer) As AnnualizedCashFlow Implements IAnnualizedCashFlowRepository.GetAnnualizedCashFlowByCategoryIdAndByMonth
        Dim resul = (From acf In Me._context.AnnualizedCashFlow Where acf.CategoryId = categoryId And acf.Month = month Select acf).FirstOrDefault()
        If resul IsNot Nothing Then
            resul.OriginalValue = (From acf In _context.AnnualizedCashFlow.AsNoTracking() Where acf.CategoryId = categoryId And acf.Month = month Select acf).FirstOrDefault()
            Return resul
        Else
            Return New AnnualizedCashFlow
        End If
    End Function

#End Region

End Class
