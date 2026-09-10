'************************************************************
' Assembly         : Infraestructure.Data.GlosasRepository
' Author           : Juan Diego Diaz
' Created          : 24-05-2013
'
' Last Modified By : Rafael Patiño
' Last Modified On : 18-06-2013
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Dynamic
Imports System.Data.Entity.Infrastructure

#End Region

''' <summary>
''' Repositorio Movimiento Glosas
''' </summary>
Public Class MovementGlosaRepository
    Inherits GenericRepository(Of GlosaMovementGlosa)
    Implements IMovementGlosaRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Función que obtiene un Movimiento Glosa especifico.
    ''' </summary>
    ''' <param name="Id">Id Movimiento Glosa</param>
    ''' <returns>Objeto Movimiento Glosa</returns>
    Public Function GetMovementGlosaById(Id As String, Optional tracking As Boolean = True) As GlosaMovementGlosa Implements IMovementGlosaRepository.GetMovementGlosaById
        If tracking Then
            Dim MovementGlosa = From e In _context.GlosaMovementGlosa.Include("GlosaMovementGlosaConciliation")
                                Where e.Id = CInt(Id)
                                Select e
            If MovementGlosa.Count > 0 Then
                Dim MovementGlosaData = MovementGlosa.SingleOrDefault
                MovementGlosaData.OriginalValue = (From e In _context.GlosaMovementGlosa.AsNoTracking
                                                   Where e.Id = CInt(Id)
                                                   Select e).SingleOrDefault
                Return MovementGlosaData
            End If
        Else
            Dim MovementGlosa = (From e In _context.GlosaMovementGlosa.AsNoTracking.Include("GlosaMovementGlosaConciliation").AsNoTracking()
                                 Where e.Id = CInt(Id)).SingleOrDefault
            If MovementGlosa IsNot Nothing Then
                Return MovementGlosa
            End If
        End If
        Return New GlosaMovementGlosa
    End Function

    ''' <summary>
    ''' Obtiene un movimiento de glosa por id con sus agregados
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetMovementGlosaByIdWithAggregates(ByVal Id As Integer) As GlosaMovementGlosa Implements IMovementGlosaRepository.GetMovementGlosaByIdWithAggregates
        Dim response = (From gmg As GlosaMovementGlosa In _context.GlosaMovementGlosa.Include("GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada")
                        Where gmg.Id = Id
                        Select gmg).FirstOrDefault
        If response IsNot Nothing Then
            Return response
        Else
            Return New GlosaMovementGlosa
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene un Movimiento Glosa especifico para reasignación de responsables.
    ''' </summary>
    ''' <param name="Id">Id Movimiento Glosa</param>
    ''' <returns>Objeto Movimiento Glosa</returns>
    Public Function GetMovementGlosaByIdTransfer(Id As String) As GlosaMovementGlosa Implements IMovementGlosaRepository.GetMovementGlosaByIdTransfer
        Dim MovementGlosa = From e In _context.GlosaMovementGlosa.Include("Responsible").Include("Responsible1").Include("GlosaInvoiceDetail").Include("GlosaInvoiceDetailQX").Include("ConceptGlosas").Include("GlosaInvoiceDetail.GlosaObjectionsReceptionD")
                            Where e.Id = CInt(Id)
                            Select e
        If MovementGlosa.Count > 0 Then
            Dim MovementGlosaData = MovementGlosa.SingleOrDefault
            MovementGlosaData.OriginalValue = (From e In _context.GlosaMovementGlosa.AsNoTracking.Include("Responsible").AsNoTracking.Include("Responsible1").AsNoTracking.Include("GlosaInvoiceDetail").AsNoTracking.Include("GlosaInvoiceDetailQX").AsNoTracking.Include("ConceptGlosas").AsNoTracking.Include("GlosaInvoiceDetail.GlosaObjectionsReceptionD").AsNoTracking
                                               Where e.Id = CInt(Id)
                                               Select e).SingleOrDefault
            Return MovementGlosaData
        End If
        Return New GlosaMovementGlosa
    End Function

    ''' <summary>
    ''' Funcion que obtiene todos los Movimientos de Glosas.
    ''' </summary>
    ''' <returns>Lista Movimiento Glosa</returns>
    Public Function ListAllMovementGlosa() As List(Of GlosaMovementGlosa) Implements IMovementGlosaRepository.ListAllMovementGlosa
        Dim Busqueda = From e In _context.GlosaMovementGlosa
                       Select e
        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' Consulta un movimiento glosa especifico.
    ''' </summary>
    ''' <param name="IdDetalleFactura">El Id del Detalle Factura</param>
    ''' <param name="IdDetalleFacturaQX">El Id del Detalle Factura QX</param>
    ''' <param name="CodigoGlosa">El Id del Codigo Glosa</param>
    ''' <returns>Objeto Movimiento Glosa</returns>
    Public Function GetMovementGlosa(ByVal IdDetalleFactura As Integer, ByVal IdDetalleFacturaQX As Integer, ByVal CodigoGlosa As Integer) As Boolean Implements IMovementGlosaRepository.GetMovementGlosa
        'Dim Busqueda As IQueryable(Of GlosaMovementGlosa)
        Dim Busqueda As Integer = 0
        If IdDetalleFacturaQX > 0 Then
            Busqueda = (From e In _context.GlosaMovementGlosa
                        Where e.InvoiceDetailId = IdDetalleFactura And e.InvoiceDetailIdQX = IdDetalleFacturaQX And e.CodeGlosa = CodigoGlosa
                        Select e).ToList().Count()
        Else
            Busqueda = (From e In _context.GlosaMovementGlosa
                        Where e.InvoiceDetailId = IdDetalleFactura And e.CodeGlosa = CodigoGlosa
                        Select e).ToList().Count()
        End If
        If Busqueda > 0 Then
            Return True
        Else
            Return False
        End If
        'If Busqueda.Count > 0 Then
        '    Return Busqueda.Single
        'Else
        '    Return New GlosaMovementGlosa
        'End If
    End Function

	''' <summary>
	''' Funcion que obtiene todos los Movimientos de Glosas según factura.
	''' </summary>
	''' <returns>Lista Movimientos Glosa</returns>
	Public Function ListAllMovementGlosaByInvoiceNumber(ByVal Invoice As String) As List(Of GlosaMovementGlosa) Implements IMovementGlosaRepository.ListAllMovementGlosaByInvoiceNumber
		Dim Busqueda = From e In _context.GlosaMovementGlosa
					   Where e.InvoiceNumber = Invoice
					   Select e

		Return Busqueda.ToList
	End Function

	''' <summary>
	''' Funcion que obtiene todos los Movimientos de Glosas y conciliaciones según factura.
	''' </summary>
	''' <returns>Lista Movimientos Glosa</returns>
	Public Function ListAllMovementGlosaAndConciliationByInvoiceNumber(ByVal Invoice As String) As List(Of GlosaMovementGlosa) Implements IMovementGlosaRepository.ListAllMovementGlosaAndConciliationByInvoiceNumber

		Dim Busqueda = From e In _context.GlosaMovementGlosa.Include("GlosaMovementGlosaConciliation")
					   Where e.InvoiceNumber = Invoice
					   Select e

		Return Busqueda.ToList
	End Function

	''' <summary>
	''' Funcion que lista los movimineto glosa por el codigo de detalle de factura
	''' </summary>
	''' <param name="InvoiceDetailId">codigo detalle de factura</param>
	''' <returns>lista de moviminetos</returns>
	Public Function ListMovementGlosa(InvoiceDetailId As String) As List(Of GlosaMovementGlosa) Implements IMovementGlosaRepository.ListMovementGlosa
        Dim Busqueda = From e In _context.GlosaMovementGlosa.Include("Responsible").Include("Responsible1").Include("ConceptGlosas")
                       Where e.InvoiceDetailId = InvoiceDetailId
                       Select e
        For Each item As GlosaMovementGlosa In Busqueda.ToList
            item.ConceptGlosas.ConceptCodeName = item.ConceptGlosas.Code & " - " & item.ConceptGlosas.NameSpecific
            item.ConceptGlosasCodeName = item.ConceptGlosas.Code & " - " & item.ConceptGlosas.NameSpecific
            item.ResponsibleReiterationName = item.Responsible?.Name
            item.ResponsibleName = item.Responsible1?.Name
        Next
        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' Funcion que lista los movimineto glosa por el codigo de detalle de factura
    ''' </summary>
    ''' <param name="InvoiceDetailId">codigo detalle de factura</param>
    '''   <param name="InvoiceDetailQXId">codigo detalle de factura QX</param>
    ''' <returns>lista de moviminetos</returns>
    Public Function ListMovementGlosaQx(InvoiceDetailId As String, InvoiceDetailQXId As String) As List(Of GlosaMovementGlosa) Implements IMovementGlosaRepository.ListMovementGlosaQx
        Dim busqueda = (From e In _context.GlosaMovementGlosa.Include("Responsible").Include("Responsible1").Include("ConceptGlosas")
                        Where e.InvoiceDetailId = InvoiceDetailId And e.InvoiceDetailIdQX = InvoiceDetailQXId
                        Select e).ToList()

        For Each item As GlosaMovementGlosa In busqueda
            item.ConceptGlosasCodeName = item.ConceptGlosas.Code & " - " & item.ConceptGlosas.NameSpecific
            item.ResponsibleReiterationName = item.Responsible?.Name
            item.ResponsibleName = item.Responsible1?.Name
        Next

        Return busqueda
    End Function

    ''' <summary>
    ''' Funcion que lista los movimiento glosa por el codigo de detalle de factura
    ''' </summary>
    ''' <param name="InvoiceDetailId">codigo detalle de factura</param>
    '''   <param name="InvoiceDetailQXId">codigo detalle de factura QX</param>
    ''' <returns>lista de movimientos</returns>
    Public Function ListMovementGlosaByCodes(InvoiceDetailId As String, InvoiceDetailQXId As String) As List(Of GlosaMovementGlosa) Implements IMovementGlosaRepository.ListMovementGlosaByCodes
        Dim Busqueda
        If CInt(InvoiceDetailQXId) = 0 Then
            Busqueda = From e In _context.GlosaMovementGlosa.Include("Responsible")
                       Where e.InvoiceDetailId = CInt(InvoiceDetailId)
                       Select e
        Else
            Busqueda = From e In _context.GlosaMovementGlosa.Include("Responsible")
                       Where e.InvoiceDetailId = CInt(InvoiceDetailId) And e.InvoiceDetailIdQX = CInt(InvoiceDetailQXId)
                       Select e
        End If
        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' Funcion que lista los movimiento glosa por el Id del Responsable
    ''' </summary>
    ''' <param name="IdResponsible">Id del Responsable</param>
    ''' <returns>lista de movimientos</returns>
    Public Function ListMovementGlosaByIdResponsible(IdResponsible As Integer) As List(Of GlosaMovementGlosa) Implements IMovementGlosaRepository.ListMovementGlosaByIdResponsible

        Dim Busqueda = From e In _context.GlosaMovementGlosa.Include("Responsible").Include("Responsible1").Include("GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaObjectionsReceptionC.Customer").Include("GlosaInvoiceDetailQX").Include("ConceptGlosas")
                       Where (e.ResponsibleId = IdResponsible AndAlso e.ResponsibleReiterationId Is Nothing) OrElse (e.ResponsibleReiterationId = IdResponsible)
                       Select e

        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' Funcion para validar que no se elimine un movimiento de una factura confirmada 
    ''' </summary>
    ''' <param name="invocieNumber">Numero de Factura</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidDeleteMovement(invocieNumber As String) As Boolean Implements IMovementGlosaRepository.ValidDeleteMovement
        Dim valid As Boolean = True
        Dim busqueda = From e In _context.GlosaObjectionsReceptionD
                       Where e.InvoiceNumber = invocieNumber And e.DocumentType = 1
                       Select e
        If busqueda.Count > 0 Then
            Dim objD As GlosaObjectionsReceptionD = busqueda.Single
            If objD.State = 2 Then
                valid = False
            Else
                valid = True
            End If
        End If
        Return valid
    End Function

    ''' <summary>
    ''' Funcion para validar que no se elimine datos de reiteracion si el oficio esta confirmado
    ''' </summary>
    ''' <param name="invocieNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateDeleteReiteration(invocieNumber As String) As Boolean Implements IMovementGlosaRepository.ValidateDeleteReiteration
        Dim valid As Boolean = True
        Dim busqueda = From d In _context.GlosaObjectionsReceptionD Join c In _context.GlosaObjectionsReceptionC On
                       c.Id Equals d.GlosaObjectionsReceptionCId
                       Where d.InvoiceNumber = invocieNumber And d.DocumentType = 2
                       Select d
        If busqueda.Count > 0 Then
            If busqueda.SingleOrDefault.State = "2" Then
                valid = False
            End If
        End If
        Return valid
    End Function

    ''' <summary>
    ''' Obtiene una lista de movimientos según Numero de factura y Código Responsable
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero Factura</param>
    ''' <param name="CodeResponsible">Código Responsable</param>
    ''' <returns>Lista de Movimientos</returns>
    Public Function ListMovementsByInvoiceAndResponsible(InvoiceNumber As String, CodeResponsible As String) As List(Of GlosaMovementGlosa) Implements IMovementGlosaRepository.ListMovementsByInvoiceAndResponsible
        Dim glosaMovementsAux As List(Of GlosaMovementGlosa) = New List(Of GlosaMovementGlosa)
        Dim glosaMovementsTotal As List(Of GlosaMovementGlosa) = New List(Of GlosaMovementGlosa)
        Dim glosaMovements As List(Of GlosaMovementGlosa) = (From c In _context.GlosaMovementGlosa.Include("GlosaInvoiceDetail").Include("GlosaInvoiceDetail.GlosaObjectionsReceptionD").Include("GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaObjectionsReceptionC").Include("GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaObjectionsReceptionC.Customer").Include("GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada").Include("ConceptGlosas").Include("GlosaInvoiceDetailQX").Include("Responsible").Include("Responsible1")
                                                             Where c.InvoiceNumber = InvoiceNumber
                                                             Select c).ToList
        If CodeResponsible = "" Then
            glosaMovements.ForEach(Function(x)
                                       If (x.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "2" Or
                                           x.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "3") Then
                                           If (x.State = "1" Or x.State = "2") Then
                                               glosaMovementsAux.Add(x)
                                           End If
                                       ElseIf (x.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "5" Or
                                          x.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "6") Then
                                           If (x.State = "3" Or x.State = "4") Then
                                               glosaMovementsAux.Add(x)
                                           End If
                                       End If
                                       Return True
                                   End Function)
        Else
            glosaMovements.ForEach(Function(x)
                                       If (x.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "2") Then
                                           If (x.State = "1" AndAlso (x.Responsible1 IsNot Nothing AndAlso x.Responsible1.CodeUser = CodeResponsible)) Then
                                               glosaMovementsAux.Add(x)
                                           End If
                                       ElseIf (x.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "5") Then
                                           If (x.State = "3" AndAlso (x.Responsible IsNot Nothing AndAlso x.Responsible.CodeUser = CodeResponsible)) Then
                                               glosaMovementsAux.Add(x)
                                           End If
                                       End If
                                       Return True
                                   End Function)
        End If
        glosaMovements.ForEach(Function(x)
                                   If (x.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "2" Or
                                       x.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "3") Then

                                       If (x.State = "1" Or x.State = "2") Then
                                           glosaMovementsTotal.Add(x)
                                       End If
                                   ElseIf (x.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "5" Or
                                      x.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "6") Then
                                       If (x.State = "3" Or x.State = "4") Then
                                           glosaMovementsTotal.Add(x)
                                       End If
                                   End If
                                   Return True
                               End Function)
        If glosaMovementsAux.Count > 0 Then
            Dim filterMovements = (From a In glosaMovementsAux
                                   Select a).ToList
            Dim i As Integer = 0
            Dim invoId As Integer = 0
            Dim invoIdQX As Integer = 0
            Dim listMovementsIds As List(Of Integer) = New List(Of Integer)
            Do
                If filterMovements(i).InvoiceDetailIdQX IsNot Nothing Then
                    If filterMovements(i).InvoiceDetailIdQX = invoIdQX AndAlso filterMovements(i).InvoiceDetailId = invoId Then

                        filterMovements.Remove(filterMovements(i))
                        i = i - 1
                    Else
                        invoId = filterMovements(i).InvoiceDetailId
                        invoIdQX = filterMovements(i).InvoiceDetailIdQX
                    End If
                End If
                i = i + 1
            Loop While i < filterMovements.Count
            For Each movement As GlosaMovementGlosa In filterMovements
                movement.GlosaInvoiceDetail.BillingGroupCodeName = movement.GlosaInvoiceDetail.BillingGroupCode + " - " + movement.GlosaInvoiceDetail.BillingGroup
                If movement.InvoiceDetailIdQX IsNot Nothing Then
                    movement.ServiceCodeName = movement.GlosaInvoiceDetailQX.ServiceCode + " - " + movement.GlosaInvoiceDetailQX.ServiceName
                Else
                    movement.ServiceCodeName = movement.GlosaInvoiceDetail.ServiceCode + " - " + movement.GlosaInvoiceDetail.ServiceName
                End If
                movement.ConceptGlosasCodeName = movement.ConceptGlosas.Code & " - " & movement.ConceptGlosas.NameSpecific
                movement.ConceptGlosas.ConceptCodeName = movement.ConceptGlosas.Code + " - " + movement.ConceptGlosas.NameSpecific
                If movement.MainGlosa = True Then
                    If movement.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "2" Or movement.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "3" Then
                        Dim max = (From a In glosaMovementsTotal
                                   Where a.InvoiceDetailId = movement.InvoiceDetailId AndAlso
                                   a.InvoiceDetailIdQX Is Nothing
                                   Select a.ValueAcceptedFirstInstance).Sum

                        Dim tmpvalueglosado = (From a In glosaMovementsTotal
                                               Where a.InvoiceDetailId = movement.InvoiceDetailId AndAlso
                                               a.InvoiceDetailIdQX Is Nothing And a.MainGlosa = True
                                               Select a.ValueGlosado).Sum

                        movement.MaxValueAccepted = tmpvalueglosado - IIf(max Is Nothing, 0, max)  'movement.ValueGlosado - IIf(max Is Nothing, 0, max)
                        movement.MaxValueAcceptedGeneral = movement.MaxValueAccepted
                        movement.ValueAux = movement.ValueAcceptedFirstInstance
                        movement.ValueGlosaMaxAccepted = "$" + movement.ValueGlosado.ToString + " - $" + movement.MaxValueAccepted.ToString
                    ElseIf movement.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "5" Or movement.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "6" Then
                        Dim max = (From a In glosaMovementsTotal
                                   Where a.InvoiceDetailId = movement.InvoiceDetailId AndAlso
                                   a.InvoiceDetailIdQX Is Nothing
                                   Select a.ValueAcceptedSecondInstance).Sum

                        Dim tmpvaluereiterated = (From a In glosaMovementsTotal
                                                  Where a.InvoiceDetailId = movement.InvoiceDetailId AndAlso
                                                  a.InvoiceDetailIdQX Is Nothing And a.MainGlosa = True
                                                  Select a.ValueReiterated).Sum

                        movement.MaxValueAccepted = tmpvaluereiterated - IIf(max Is Nothing, 0, max)  'movement.ValueGlosado - IIf(max Is Nothing, 0, max) - If(movement.ValueAcceptedFirstInstance Is Nothing, 0, movement.ValueAcceptedFirstInstance)
                        movement.MaxValueAcceptedGeneral = movement.MaxValueAccepted
                        movement.ValueAux = movement.ValueAcceptedSecondInstance
                        movement.ValueReiteratedMaxAccepted = "$" + movement.ValueReiterated.ToString + " - $" + movement.MaxValueAccepted.ToString
                    End If
                Else
                    Dim maxValue
                    If movement.InvoiceDetailIdQX Is Nothing Then
                        maxValue = (From b In filterMovements
                                    Where b.InvoiceDetailId = movement.InvoiceDetailId _
                                    AndAlso b.InvoiceDetailIdQX Is Nothing _
                                    AndAlso b.MainGlosa = True
                                    Select b.ValueGlosado, b.ValueReiterated).FirstOrDefault
                    Else
                        maxValue = (From b In filterMovements
                                    Where b.InvoiceDetailId = movement.InvoiceDetailId _
                                    AndAlso b.InvoiceDetailIdQX = movement.InvoiceDetailIdQX _
                                    AndAlso b.MainGlosa = True
                                    Select b.ValueGlosado, b.ValueReiterated).FirstOrDefault
                    End If
                    If maxValue Is Nothing Then
                        If movement.InvoiceDetailIdQX Is Nothing Then
                            maxValue = (From b In glosaMovementsTotal
                                        Where b.InvoiceDetailId = movement.InvoiceDetailId _
                                        AndAlso b.InvoiceDetailIdQX Is Nothing _
                                        AndAlso b.MainGlosa = True
                                        Select b.Responsible1, b.Responsible, b.ResponsibleId, b.ResponsibleReiterationId, b.ValueGlosado, b.ValueReiterated).FirstOrDefault
                        Else
                            maxValue = (From b In glosaMovementsTotal
                                        Where b.InvoiceDetailId = movement.InvoiceDetailId _
                                        AndAlso b.InvoiceDetailIdQX = movement.InvoiceDetailIdQX _
                                        AndAlso b.MainGlosa = True
                                        Select b.Responsible1, b.Responsible, b.ResponsibleId, b.ResponsibleReiterationId, b.ValueGlosado, b.ValueReiterated).FirstOrDefault
                        End If
                    End If
                    If movement.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "2" Or movement.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "3" Then
                        Dim max = (From a In glosaMovementsTotal
                                   Where a.InvoiceDetailId = movement.InvoiceDetailId AndAlso
                                   a.InvoiceDetailIdQX Is Nothing
                                   Select a.ValueAcceptedFirstInstance).Sum

                        Dim tmpvalueglosado = (From a In glosaMovementsTotal
                                               Where a.InvoiceDetailId = movement.InvoiceDetailId AndAlso
                                               a.InvoiceDetailIdQX Is Nothing And a.MainGlosa = True
                                               Select a.ValueGlosado).Sum

                        movement.MaxValueAccepted = tmpvalueglosado - IIf(max Is Nothing, 0, max)
                        movement.MaxValueAcceptedGeneral = movement.MaxValueAccepted
                        movement.ValueAux = movement.ValueAcceptedFirstInstance
                        movement.ValueGlosaMaxAccepted = "$" + movement.ValueGlosado.ToString + " - $" + movement.MaxValueAccepted.ToString
                    ElseIf movement.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "5" Or movement.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "6" Then
                        Dim max = (From a In glosaMovementsTotal
                                   Where a.InvoiceDetailId = movement.InvoiceDetailId AndAlso
                                   a.InvoiceDetailIdQX Is Nothing
                                   Select a.ValueAcceptedSecondInstance).Sum

                        Dim tmpvaluereiterated = (From a In glosaMovementsTotal
                                                  Where a.InvoiceDetailId = movement.InvoiceDetailId AndAlso
                                                  a.InvoiceDetailIdQX Is Nothing And a.MainGlosa = True
                                                  Select a.ValueReiterated).Sum

                        movement.MaxValueAccepted = tmpvaluereiterated - IIf(max Is Nothing, 0, max)
                        movement.MaxValueAcceptedGeneral = movement.MaxValueAccepted
                        movement.ValueAux = movement.ValueAcceptedSecondInstance
                        movement.ValueReiteratedMaxAccepted = "$" + movement.ValueReiterated.ToString + " - $" + movement.MaxValueAccepted.ToString
                    End If
                End If
                Dim consulta
                If movement.InvoiceDetailIdQX Is Nothing Then
                    consulta = (From a In glosaMovementsTotal
                                Where (a.InvoiceDetailId = movement.InvoiceDetailId AndAlso a.InvoiceDetailIdQX Is Nothing) AndAlso a.Id <> movement.Id
                                Select a).ToList
                Else
                    consulta = (From a In glosaMovementsTotal
                                Where (a.InvoiceDetailId = movement.InvoiceDetailId AndAlso a.InvoiceDetailIdQX = movement.InvoiceDetailIdQX) AndAlso a.Id <> movement.Id
                                Select a).ToList
                End If
                For Each consultMov As GlosaMovementGlosa In consulta
                    consultMov.ConceptGlosas.ConceptCodeName = consultMov.ConceptGlosas.Code + " - " + consultMov.ConceptGlosas.NameSpecific
                    consultMov.ConceptGlosasCodeName = consultMov.ConceptGlosas.Code & " - " & consultMov.ConceptGlosas.NameSpecific
                    If consultMov.Responsible1 IsNot Nothing Then
                        consultMov.ResponsibleCodeNameGlosa = consultMov.Responsible1.CodeUser + " - " + consultMov.Responsible1.Name
                    End If
                    If consultMov.Responsible IsNot Nothing Then
                        consultMov.ResponsibleCodeNameReiteration = consultMov.Responsible.CodeUser + " - " + consultMov.Responsible.Name
                    End If
                Next
                movement.OtherMovements = consulta
                movement.ListMovimientoAux = (From c In glosaMovementsAux
                                              Where c.InvoiceDetailId = movement.InvoiceDetailId AndAlso c.InvoiceDetailIdQX = movement.InvoiceDetailIdQX
                                              Select c).ToList
                If movement.ListMovimientoAux IsNot Nothing Then
                    For Each itemMovAux As GlosaMovementGlosa In movement.ListMovimientoAux
                        If itemMovAux.MainGlosa = True Then
                            If itemMovAux.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "2" Or movement.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "3" Then
                                Dim max = (From a In glosaMovementsTotal
                                           Where a.InvoiceDetailId = itemMovAux.InvoiceDetailId AndAlso
                                           a.InvoiceDetailIdQX = itemMovAux.InvoiceDetailIdQX
                                           Select a.ValueAcceptedFirstInstance).Sum

                                Dim tmpvalueglosado = (From a In glosaMovementsTotal
                                                       Where a.InvoiceDetailId = itemMovAux.InvoiceDetailId AndAlso
                                                       a.InvoiceDetailIdQX = itemMovAux.InvoiceDetailIdQX And a.MainGlosa = True
                                                       Select a.ValueGlosado).Sum

                                itemMovAux.MaxValueAccepted = tmpvalueglosado - IIf(max Is Nothing, 0, max)
                                itemMovAux.MaxValueAcceptedGeneral = itemMovAux.MaxValueAccepted
                                itemMovAux.ValueAux = itemMovAux.ValueAcceptedFirstInstance
                                itemMovAux.ValueGlosaMaxAccepted = "$" + itemMovAux.ValueGlosado.ToString + " - $" + itemMovAux.MaxValueAccepted.ToString
                            ElseIf itemMovAux.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "5" Or movement.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "6" Then
                                Dim max = (From a In glosaMovementsTotal
                                           Where a.InvoiceDetailId = itemMovAux.InvoiceDetailId AndAlso
                                           a.InvoiceDetailIdQX = itemMovAux.InvoiceDetailIdQX
                                           Select a.ValueAcceptedSecondInstance).Sum

                                Dim tmpvaluereiterated = (From a In glosaMovementsTotal
                                                          Where a.InvoiceDetailId = itemMovAux.InvoiceDetailId AndAlso
                                                          a.InvoiceDetailIdQX = itemMovAux.InvoiceDetailIdQX And a.MainGlosa = True
                                                          Select a.ValueReiterated).Sum

                                itemMovAux.MaxValueAccepted = tmpvaluereiterated - IIf(max Is Nothing, 0, max) '- If(itemMovAux.ValueAcceptedFirstInstance Is Nothing, 0, itemMovAux.ValueAcceptedFirstInstance)
                                itemMovAux.MaxValueAcceptedGeneral = itemMovAux.MaxValueAccepted
                                itemMovAux.ValueAux = itemMovAux.ValueAcceptedSecondInstance
                                itemMovAux.ValueReiteratedMaxAccepted = "$" + itemMovAux.ValueReiterated.ToString + " - $" + itemMovAux.MaxValueAccepted.ToString
                            End If
                        Else
                            Dim maxValue
                            maxValue = (From b In filterMovements
                                        Where b.InvoiceDetailId = itemMovAux.InvoiceDetailId _
                                        AndAlso b.InvoiceDetailIdQX = itemMovAux.InvoiceDetailIdQX _
                                        AndAlso b.MainGlosa = True
                                        Select b.ValueGlosado, b.ValueReiterated).FirstOrDefault
                            If maxValue Is Nothing Then
                                maxValue = (From b In glosaMovementsTotal
                                            Where b.InvoiceDetailId = itemMovAux.InvoiceDetailId _
                                            AndAlso b.InvoiceDetailIdQX = itemMovAux.InvoiceDetailIdQX _
                                            AndAlso b.MainGlosa = True
                                            Select b.ValueGlosado, b.ValueReiterated).FirstOrDefault
                            End If
                            If itemMovAux.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "2" Or movement.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "3" Then
                                Dim max = (From a In glosaMovementsTotal
                                           Where a.InvoiceDetailId = itemMovAux.InvoiceDetailId AndAlso
                                           a.InvoiceDetailIdQX = itemMovAux.InvoiceDetailIdQX
                                           Select a.ValueAcceptedFirstInstance).Sum

                                Dim tmpvalueglosado = (From a In glosaMovementsTotal
                                                       Where a.InvoiceDetailId = itemMovAux.InvoiceDetailId AndAlso
                                                       a.InvoiceDetailIdQX = itemMovAux.InvoiceDetailIdQX And a.MainGlosa = True
                                                       Select a.ValueGlosado).Sum

                                itemMovAux.MaxValueAccepted = tmpvalueglosado - IIf(max Is Nothing, 0, max)
                                itemMovAux.MaxValueAcceptedGeneral = itemMovAux.MaxValueAccepted
                                itemMovAux.ValueAux = itemMovAux.ValueAcceptedFirstInstance
                                itemMovAux.ValueGlosaMaxAccepted = "$" + itemMovAux.ValueGlosado.ToString + " - $" + itemMovAux.MaxValueAccepted.ToString
                            ElseIf itemMovAux.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "5" Or movement.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = "6" Then
                                Dim max = (From a In glosaMovementsTotal
                                           Where a.InvoiceDetailId = itemMovAux.InvoiceDetailId AndAlso
                                           a.InvoiceDetailIdQX = itemMovAux.InvoiceDetailIdQX
                                           Select a.ValueAcceptedSecondInstance).Sum

                                Dim tmpvaluereiterated = (From a In glosaMovementsTotal
                                                          Where a.InvoiceDetailId = itemMovAux.InvoiceDetailId AndAlso
                                                          a.InvoiceDetailIdQX = itemMovAux.InvoiceDetailIdQX And a.MainGlosa = True
                                                          Select a.ValueReiterated).Sum

                                itemMovAux.MaxValueAccepted = tmpvaluereiterated - IIf(max Is Nothing, 0, max)  'maxValue.ValueGlosado - IIf(max Is Nothing, 0, max) - If(itemMovAux.ValueAcceptedFirstInstance Is Nothing, 0, itemMovAux.ValueAcceptedFirstInstance)
                                itemMovAux.MaxValueAcceptedGeneral = itemMovAux.MaxValueAccepted
                                itemMovAux.ValueAux = itemMovAux.ValueAcceptedSecondInstance
                                itemMovAux.ValueReiteratedMaxAccepted = "$" + itemMovAux.ValueReiterated.ToString + " - $" + itemMovAux.MaxValueAccepted.ToString
                            End If
                        End If
                        itemMovAux.GlosaInvoiceDetail.BillingGroupCodeName = itemMovAux.GlosaInvoiceDetail.BillingGroupCode + " - " + itemMovAux.GlosaInvoiceDetail.BillingGroup
                        itemMovAux.ServiceCodeName = itemMovAux.GlosaInvoiceDetailQX.ServiceCode + " - " + itemMovAux.GlosaInvoiceDetailQX.ServiceName
                        itemMovAux.ConceptGlosas.ConceptCodeName = itemMovAux.ConceptGlosas.Code + " - " + itemMovAux.ConceptGlosas.NameSpecific
                        itemMovAux.ConceptGlosasCodeName = itemMovAux.ConceptGlosas.Code & " - " & itemMovAux.ConceptGlosas.NameSpecific
                        Dim consultaQX
                        If itemMovAux.InvoiceDetailIdQX Is Nothing Then
                            consultaQX = (From a In glosaMovementsTotal
                                          Where (a.InvoiceDetailId = itemMovAux.InvoiceDetailId AndAlso a.InvoiceDetailIdQX Is Nothing) AndAlso a.Id <> itemMovAux.Id
                                          Select a).ToList
                        Else
                            consultaQX = (From a In glosaMovementsTotal
                                          Where (a.InvoiceDetailId = itemMovAux.InvoiceDetailId AndAlso a.InvoiceDetailIdQX = itemMovAux.InvoiceDetailIdQX) AndAlso a.Id <> itemMovAux.Id
                                          Select a).ToList
                        End If
                        For Each consultMovQX As GlosaMovementGlosa In consultaQX
                            consultMovQX.ConceptGlosas.ConceptCodeName = consultMovQX.ConceptGlosas.Code + " - " + consultMovQX.ConceptGlosas.NameSpecific
                            consultMovQX.ConceptGlosasCodeName = consultMovQX.ConceptGlosas.Code & " - " & consultMovQX.ConceptGlosas.NameSpecific
                            If consultMovQX.Responsible1 IsNot Nothing Then
                                consultMovQX.ResponsibleCodeNameGlosa = consultMovQX.Responsible1.CodeUser + " - " + consultMovQX.Responsible1.Name
                            End If
                            If consultMovQX.Responsible IsNot Nothing Then
                                consultMovQX.ResponsibleCodeNameReiteration = consultMovQX.Responsible.CodeUser + " - " + consultMovQX.Responsible.Name
                            End If
                        Next
                        itemMovAux.OtherMovements = consultaQX
                    Next
                End If

                If movement.IdGlosaEvaluation IsNot Nothing Then
                    Dim glosaConcept = (From x In _context.ConceptGlosas.AsNoTracking() Where x.Id = movement.IdGlosaEvaluation Select x)?.FirstOrDefault
                    If glosaConcept IsNot Nothing Then
                        movement.GlosaEvaluationType = glosaConcept.HomologateTypeByCodeAndResponse()
                    End If
                End If

            Next
            Return filterMovements
        Else
            Return New List(Of GlosaMovementGlosa)
        End If

    End Function

    ''' <summary>
    ''' Validar si existen movimientos sin confirmar que no permitan confirmar una factura
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de Factura</param>
    ''' <returns>Boolean</returns>
    Public Function ValidateInvoicesMovements(InvoiceNumber As String) As Boolean Implements IMovementGlosaRepository.ValidateInvoicesMovements
        Dim consulta = (From d In _context.GlosaMovementGlosa
                        Where d.InvoiceNumber = InvoiceNumber AndAlso ((d.State = "1") Or (d.State = "3"))
                        Select d.Id)
        If consulta.Count() > 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Funcion para listar movimientos de glosas para las evaluaciones masivas a nivel de facturas
    ''' </summary>
    ''' <param name="ListInvoice">Lista de Facturas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllMovementGlosabymultipleInvoice(ByVal ListInvoice As List(Of String), Optional ByVal codeUser As String = "") As List(Of GlosaMovementGlosa) Implements IMovementGlosaRepository.ListAllMovementGlosabymultipleInvoice
        If codeUser = String.Empty Then
            Dim busqueda = From e In _context.GlosaMovementGlosa.Include("GlosaInvoiceDetail").Include("GlosaInvoiceDetail.GlosaInvoiceDetailQX").Include("GlosaInvoiceDetail.GlosaObjectionsReceptionD").Include("GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaObjectionsReceptionC").Include("GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaObjectionsReceptionC.Customer").Include("GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada")
                           Where ListInvoice.Contains(e.InvoiceNumber)
                           Select e
            Return busqueda.ToList
        Else
            Dim busqueda = From e In _context.GlosaMovementGlosa.Include("GlosaInvoiceDetail").Include("GlosaInvoiceDetail.GlosaObjectionsReceptionD").Include("GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaObjectionsReceptionC").Include("GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaObjectionsReceptionC.Customer").Include("GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada")
                           Where ListInvoice.Contains(e.InvoiceNumber) And (e.Responsible1.CodeUser = codeUser Or e.Responsible.CodeUser = codeUser)
                           Select e
            Return busqueda.ToList
        End If

    End Function

    ''' <summary>
    ''' Funcion que lista los movimineto glosa por toda la factura
    ''' </summary>
    ''' <param name="InvoiceNumber">numero de factura</param>
    ''' <returns>lista de moviminetos</returns>
    Public Function ListMovementGlosaByInvoiceNumber(InvoiceNumber As String) As List(Of GlosaMovementGlosa) Implements IMovementGlosaRepository.ListMovementGlosaByInvoiceNumber
        Dim Busqueda = From e In _context.GlosaMovementGlosa
                       Where e.InvoiceNumber = InvoiceNumber
                       Select e
        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' Funcion que lista los movimineto glosa por toda la factura
    ''' </summary>
    ''' <param name="InvoiceNumber">numero de factura</param>
    ''' <returns>lista de moviminetos</returns>
    Public Function ListMovementGlosaByInvoiceNumberWithAggregates(InvoiceNumber As String) As List(Of GlosaMovementGlosa) Implements IMovementGlosaRepository.ListMovementGlosaByInvoiceNumberWithAggregates
        Dim Busqueda = From e In _context.GlosaMovementGlosa.Include("PartialPaymentsMovement")
                       Where e.InvoiceNumber = InvoiceNumber
                       Select e
        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' Funcion para retornar lista de objeto que totalizan y agrupa la aceptaciones de IPS en primera Instancia o glosa
    ''' </summary>
    ''' <param name="Invoicenumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListExpanDObj_AcceptedIPSFirtsInstance(ByVal Invoicenumber As String, ByVal affectsService As Boolean) As List(Of Object) Implements IMovementGlosaRepository.ListExpanDObj_AcceptedIPSFirtsInstance
        Dim list As New List(Of Object)()
        Dim resqx = (From TMovement In Me._context.GlosaMovementGlosa
                     Join TDetailQX In Me._context.GlosaInvoiceDetailQX On TDetailQX.Id Equals TMovement.InvoiceDetailIdQX
                     Join TAccount In Me._context.MainAccounts On TAccount.Number Equals TDetailQX.AccountantAccountIncome
                     Join LegalBook In Me._context.LegalBook On LegalBook.Id Equals TAccount.LegalBookId
                     Join TCost In Me._context.CostCenter On TCost.Code Equals TDetailQX.CostCenterCode
                     Join GlosaInvoiceD In Me._context.GlosaInvoiceDetail On TDetailQX.InvoiceDetailId Equals GlosaInvoiceD.Id
                     Join id In Me._context.InvoiceDetail On GlosaInvoiceD.InvoiceDetailNativeId Equals id.Id
                     Group Join tax In Me._context.GeneralLedgerIVA On id.TaxId Equals tax.Id Into Group
                     From tax In Group.DefaultIfEmpty()
                     Where TMovement.InvoiceNumber.Equals(Invoicenumber) And TMovement.ValueAcceptedFirstInstance > 0 And TMovement.State <> 6 And LegalBook.OfficialBook = True
                     Group By _EntityName = "ServiceOrderDetailSurgical",
                             _EntityId = TDetailQX.ServiceOrderDetailSurgicalId,
                             _MainAccountId = TAccount.Id,
                             _CostCenterId = TCost.Id,
                             _TaxPercent = If(tax Is Nothing, 0, tax.Percentage),
                             _TaxId = id.TaxId,
                            _IdMoven = TMovement.Id
                         Into mygroup = Group, _ValueAcceptedFirstInstance = Sum(TMovement.ValueAcceptedFirstInstance)
                     Select New With {
                                    .GlosaMovementGlosaId = _IdMoven,
                                    .Value = _ValueAcceptedFirstInstance,
                                    .MainAccountId = _MainAccountId,
                                    .CostCenterId = _CostCenterId,
                                    .EntityName = _EntityName,
                                    .EntityId = _EntityId,
                                    .TaxPercent = _TaxPercent,
                                    .TaxId = _TaxId
                                    }).ToList()

        Dim res = (From TMovement In Me._context.GlosaMovementGlosa
                   Join TDetail In Me._context.GlosaInvoiceDetail On TDetail.Id Equals TMovement.InvoiceDetailId
                   Join TAccount In Me._context.MainAccounts On TAccount.Number Equals TDetail.AccountantAccountIncome
                   Join LegalBook In Me._context.LegalBook On LegalBook.Id Equals TAccount.LegalBookId
                   Join TCost In Me._context.CostCenter On TCost.Code Equals TDetail.CostCenterCode
                   Join id In Me._context.InvoiceDetail On TDetail.InvoiceDetailNativeId Equals id.Id
                   Group Join tax In Me._context.GeneralLedgerIVA On id.TaxId Equals tax.Id Into Group
                   From tax In Group.DefaultIfEmpty()
                   Where TMovement.InvoiceNumber.Equals(Invoicenumber) And TMovement.ValueAcceptedFirstInstance > 0 And TMovement.State <> 6 And TMovement.InvoiceDetailIdQX.Equals(Nothing) And LegalBook.OfficialBook = True
                   Group By _EntityName = "InvoiceDetail",
                            _EntityId = TDetail.InvoiceDetailNativeId, _MainAccountId = TAccount.Id,
                            _CostCenterId = TCost.Id,
                            _TaxPercent = If(tax Is Nothing, 0, tax.Percentage),
                            _TaxId = id.TaxId,
                            _IdMoven = TMovement.Id
                            Into mygroup = Group, _ValueAcceptedFirstInstance = Sum(TMovement.ValueAcceptedFirstInstance)
                   Select New With {
                                    .GlosaMovementGlosaId = _IdMoven,
                                    .Value = _ValueAcceptedFirstInstance,
                                    .MainAccountId = _MainAccountId,
                                    .CostCenterId = _CostCenterId,
                                    .EntityName = _EntityName,
                                    .EntityId = _EntityId,
                                    .TaxPercent = _TaxPercent,
                                    .TaxId = _TaxId
                                    }).ToList()
        Dim unionres = res.Union(resqx)
            For Each d In unionres
            list.Add(New ExpandoObject())
            list(list.Count - 1).GlosaMovementGlosaId = d.GlosaMovementGlosaId
            list(list.Count - 1).Value = d.Value
            list(list.Count - 1).MainAccountId = d.MainAccountId
            list(list.Count - 1).CostCenterId = d.CostCenterId
            list(list.Count - 1).EntityName = d.EntityName
            list(list.Count - 1).EntityId = d.EntityId
            list(list.Count - 1).TaxPercent = d.TaxPercent
            list(list.Count - 1).TaxId = d.TaxId
        Next
            Return list
    End Function

    ''' <summary>
    ''' Funcion para retornar lista de objeto que totalizan y agrupa la aceptaciones de IPS en Segunda Instancia o reiteracione
    ''' </summary>
    ''' <param name="Invoicenumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListExpanDObj_AcceptedIPSSecondInstance(ByVal Invoicenumber As String, ByVal affectsService As Boolean) As List(Of Object) Implements IMovementGlosaRepository.ListExpanDObj_AcceptedIPSSecondInstance
        Dim list As New List(Of Object)()
        Dim resqx = (From TMovement In Me._context.GlosaMovementGlosa
                     Join TDetailQX In Me._context.GlosaInvoiceDetailQX On TDetailQX.Id Equals TMovement.InvoiceDetailIdQX
                     Join TAccount In Me._context.MainAccounts On TAccount.Number Equals TDetailQX.AccountantAccountIncome
                     Join LegalBook In Me._context.LegalBook On LegalBook.Id Equals TAccount.LegalBookId
                     Join TCost In Me._context.CostCenter On TCost.Code Equals TDetailQX.CostCenterCode
                     Join GlosaInvoiceD In Me._context.GlosaInvoiceDetail On TDetailQX.InvoiceDetailId Equals GlosaInvoiceD.Id
                     Join id In Me._context.InvoiceDetail On GlosaInvoiceD.InvoiceDetailNativeId Equals id.Id
                     Group Join tax In Me._context.GeneralLedgerIVA On id.TaxId Equals tax.Id Into Group
                     From tax In Group.DefaultIfEmpty()
                     Where TMovement.InvoiceNumber.Equals(Invoicenumber) And TMovement.ValueAcceptedSecondInstance > 0 And TMovement.State <> 6 And LegalBook.OfficialBook = True
                     Group By _EntityName = "ServiceOrderDetailSurgical",
                                _EntityId = TDetailQX.ServiceOrderDetailSurgicalId,
                                _MainAccountId = TAccount.Id,
                                _CostCenterId = TCost.Id,
                                _TaxPercent = If(tax Is Nothing, 0, tax.Percentage),
                                _TaxId = id.TaxId
                         Into mygroup = Group, _ValueAcceptedSecondInstance = Sum(TMovement.ValueAcceptedSecondInstance)
                     Select New With {.Value = _ValueAcceptedSecondInstance,
                                        .MainAccountId = _MainAccountId,
                                        .CostCenterId = _CostCenterId,
                                        .EntityName = _EntityName,
                                        .EntityId = _EntityId,
                                        .TaxPercent = _TaxPercent,
                                        .TaxId = _TaxId
                                      }).ToList()

        Dim res = (From TMovement In Me._context.GlosaMovementGlosa
                   Join TDetail In Me._context.GlosaInvoiceDetail On TDetail.Id Equals TMovement.InvoiceDetailId
                   Join TAccount In Me._context.MainAccounts On TAccount.Number Equals TDetail.AccountantAccountIncome
                   Join LegalBook In Me._context.LegalBook On LegalBook.Id Equals TAccount.LegalBookId
                   Join TCost In Me._context.CostCenter On TCost.Code Equals TDetail.CostCenterCode
                   Join id In Me._context.InvoiceDetail On TDetail.InvoiceDetailNativeId Equals id.Id
                   Group Join tax In Me._context.GeneralLedgerIVA On id.TaxId Equals tax.Id Into Group
                   From tax In Group.DefaultIfEmpty()
                   Where TMovement.InvoiceNumber.Equals(Invoicenumber) And TMovement.ValueAcceptedSecondInstance > 0 And TMovement.State <> 6 And TMovement.InvoiceDetailIdQX.Equals(Nothing) And LegalBook.OfficialBook = True
                   Group By _EntityName = "InvoiceDetail",
                            _EntityId = TDetail.InvoiceDetailNativeId,
                            _MainAccountId = TAccount.Id,
                            _CostCenterId = TCost.Id,
                            _TaxPercent = If(tax Is Nothing, 0, tax.Percentage),
                            _TaxId = id.TaxId
                       Into mygroup = Group, _ValueAcceptedSecondInstance = Sum(TMovement.ValueAcceptedSecondInstance)
                   Select New With {.Value = _ValueAcceptedSecondInstance,
                                        .MainAccountId = _MainAccountId,
                                        .CostCenterId = _CostCenterId,
                                        .EntityName = _EntityName,
                                        .EntityId = _EntityId,
                                        .TaxPercent = _TaxPercent,
                                        .TaxId = _TaxId
                                        }).ToList()
        Dim unionres = res.Union(resqx)
        For Each d In unionres
            list.Add(New ExpandoObject())
            list(list.Count - 1).Value = d.Value
            list(list.Count - 1).MainAccountId = d.MainAccountId
            list(list.Count - 1).CostCenterId = d.CostCenterId
            list(list.Count - 1).EntityName = d.EntityName
            list(list.Count - 1).EntityId = d.EntityId
            list(list.Count - 1).TaxPercent = d.TaxPercent
            list(list.Count - 1).TaxId = d.TaxId
        Next
        Return list
    End Function

    ''' <summary>
    ''' Funcion para retornar lista de objeto que totalizan y agrupa la aceptaciones de IPS en Conciliacion
    ''' </summary>
    ''' <param name="Invoicenumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListExpanDObj_AcceptedIPSConciliation(ByVal Invoicenumber As String, ByVal ConciliationCId As Integer, ByVal affectsService As Boolean) As List(Of Object) Implements IMovementGlosaRepository.ListExpanDObj_AcceptedIPSConciliation
        Dim list As New List(Of Object)()
        Dim Subquery = From gmgc In Me._context.GlosaMovementGlosaConciliation
                       Group By ConciliationId = gmgc.ConciliationCId, GlosaMovementGlosaId = gmgc.GlosaMovementGlosaId Into Mygrop = Group, ValueAccepttedIPS = Sum(gmgc.ValueAcceptedIPSconciliation)

        Dim resqx = (From TMovement In Me._context.GlosaMovementGlosa
                     Join TConciliationMovement In Subquery On TConciliationMovement.ConciliationId Equals TMovement.ConciliationCId And TConciliationMovement.GlosaMovementGlosaId Equals TMovement.Id
                     Join TDetailQX In Me._context.GlosaInvoiceDetailQX On TDetailQX.Id Equals TMovement.InvoiceDetailIdQX
                     Join TAccount In Me._context.MainAccounts On TAccount.Number Equals TDetailQX.AccountantAccountIncome
                     Join LegalBook In Me._context.LegalBook On LegalBook.Id Equals TAccount.LegalBookId
                     Join TCost In Me._context.CostCenter On TCost.Code Equals TDetailQX.CostCenterCode
                     Join GlosaInvoiceD In Me._context.GlosaInvoiceDetail On TDetailQX.InvoiceDetailId Equals GlosaInvoiceD.Id
                     Join id In Me._context.InvoiceDetail On GlosaInvoiceD.InvoiceDetailNativeId Equals id.Id
                     Group Join tax In Me._context.GeneralLedgerIVA On id.TaxId Equals tax.Id Into Group
                     From tax In Group.DefaultIfEmpty()
                     Where TMovement.InvoiceNumber.Equals(Invoicenumber) And TMovement.ValueAcceptedIPSconciliation > 0 And TMovement.ConciliationCId = ConciliationCId And LegalBook.OfficialBook = True
                     Group By _EntityName = "ServiceOrderDetailSurgical",
                                    _EntityId = TDetailQX.ServiceOrderDetailSurgicalId,
                                    _MainAccountId = TAccount.Id,
                                    _CostCenterId = TCost.Id,
                                    _TaxPercent = If(tax Is Nothing, 0, tax.Percentage),
                                    _TaxId = id.TaxId
                             Into mygroup = Group, _ValueAcceptedIPSconciliation = Sum(TConciliationMovement.ValueAccepttedIPS)
                     Select New With {.Value = _ValueAcceptedIPSconciliation,
                                          .MainAccountId = _MainAccountId,
                                          .CostCenterId = _CostCenterId,
                                          .EntityName = _EntityName,
                                          .EntityId = _EntityId,
                                          .TaxPercent = _TaxPercent,
                                          .TaxId = _TaxId
                                          }).ToList()

        Dim res = (From TMovement In Me._context.GlosaMovementGlosa
                       Join TConciliationMovement In Subquery On TConciliationMovement.ConciliationId Equals TMovement.ConciliationCId And TConciliationMovement.GlosaMovementGlosaId Equals TMovement.Id
                       Join TDetail In Me._context.GlosaInvoiceDetail On TDetail.Id Equals TMovement.InvoiceDetailId
                       Join TAccount In Me._context.MainAccounts On TAccount.Number Equals TDetail.AccountantAccountIncome
                       Join LegalBook In Me._context.LegalBook On LegalBook.Id Equals TAccount.LegalBookId
                       Join TCost In Me._context.CostCenter On TCost.Code Equals TDetail.CostCenterCode
                       Join id In Me._context.InvoiceDetail On TDetail.InvoiceDetailNativeId Equals id.Id
                       Group Join tax In Me._context.GeneralLedgerIVA On id.TaxId Equals tax.Id Into Group
                       From tax In Group.DefaultIfEmpty()
                       Where TMovement.InvoiceNumber.Equals(Invoicenumber) And TMovement.ValueAcceptedIPSconciliation > 0 And TMovement.ConciliationCId = ConciliationCId And TMovement.InvoiceDetailIdQX.Equals(Nothing) And LegalBook.OfficialBook = True
                       Group By _EntityName = "InvoiceDetail",
                           _EntityId = TDetail.InvoiceDetailNativeId,
                           _MainAccountId = TAccount.Id,
                           _CostCenterId = TCost.Id,
                           _TaxPercent = If(tax Is Nothing, 0, tax.Percentage),
                           _TaxId = id.TaxId
                           Into mygroup = Group, _ValueAcceptedIPSconciliation = Sum(TConciliationMovement.ValueAccepttedIPS)
                       Select New With {.Value = _ValueAcceptedIPSconciliation,
                                        .MainAccountId = _MainAccountId,
                                        .CostCenterId = _CostCenterId,
                                        .EntityName = _EntityName,
                                        .EntityId = _EntityId,
                                        .TaxPercent = _TaxPercent,
                                        .TaxId = _TaxId
                                        }).ToList()
            Dim unionres = res.Union(resqx)
            For Each d In unionres
                list.Add(New ExpandoObject())
                list(list.Count - 1).Value = d.Value
                list(list.Count - 1).MainAccountId = d.MainAccountId
                list(list.Count - 1).CostCenterId = d.CostCenterId
                list(list.Count - 1).EntityName = d.EntityName
                list(list.Count - 1).EntityId = d.EntityId
                list(list.Count - 1).TaxPercent = d.TaxPercent
                list(list.Count - 1).TaxId = d.TaxId
            Next

            Return list
    End Function

    Public Function SP_ImportGlosaMovementGlosas(XmlObject As String) As List(Of SP_ImportGlosaMovementGlosas_Result) Implements IMovementGlosaRepository.SP_ImportGlosaMovementGlosas
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ImportGlosaMovementGlosas(XmlObject).ToList
    End Function

End Class