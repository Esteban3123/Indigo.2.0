'***********************************************************************
' Assembly         : Infrestructure.Data.GlosasRepository
' Author           : RafaelPatiño
' Created          : 09-04-2013
'
' Last Modified By : RafaelPatiño
' Last Modified On : 21-04-2013
'
' Copyright        : (c) . All rights reserved.
'*********************************************************************** 

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class ObjectionsReceptionDRepository
    Inherits GenericRepository(Of GlosaObjectionsReceptionD)
    Implements IObjectionsReceptionDRepository


    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la nueva instancia de <see cref="ObjectionsReceptionDRepository" /> clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Lista de todos los item del detalle de una objecion recepcionada
    ''' </summary>
    ''' <param name="objectionReceptionId">recibe el id de la recepción de objecion</param>
    ''' <returns>una lista de item del detalle de una objecion recepcionada</returns>
    Public Function ListAllObjectionsReceptionDWithIncludes(ByVal objectionReceptionId As Integer) As List(Of GlosaObjectionsReceptionD) Implements IObjectionsReceptionDRepository.ListAllObjectionsReceptionDWithIncludes
        Dim Busqueda = From e In _context.GlosaObjectionsReceptionD.Include("GlosaPortfolioGlosada")
                       Where e.GlosaObjectionsReceptionCId = objectionReceptionId
                       Select e

        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' lista de detalle de una objecion de recepción
    ''' </summary>
    ''' <param name="codeObjectionReceptionC">codigo de la recepción</param>
    ''' <returns>una lista de detalle de la recepcion de una objeción </returns>
    ''' <remarks></remarks>
    Public Function ListAllObjectionsReceptionD(codeObjectionReceptionC As String) As List(Of GlosaObjectionsReceptionD) Implements IObjectionsReceptionDRepository.ListAllObjectionsReceptionD
        Dim Busqueda = From e In _context.GlosaObjectionsReceptionD.Include("GlosaObjectionsReceptionC")
                       Where e.GlosaObjectionsReceptionCId = CInt(codeObjectionReceptionC)
                       Select e

        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' Retorna un Objeto factura
    ''' </summary>
    ''' <param name="Code">Codigo ID</param>
    ''' <returns>Objeto Factura</returns>
    Public Function getObjectionReceptionDById(Code As String, Optional tracking As Boolean = True) As GlosaObjectionsReceptionD Implements IObjectionsReceptionDRepository.getObjectionReceptionDById
        Dim Busqueda = Nothing
        If tracking = False Then
            Busqueda = (From e In _context.GlosaObjectionsReceptionD.AsNoTracking.Include("GlosaPortfolioGlosada").AsNoTracking.Include("GlosaObjectionsReceptionC").AsNoTracking
              Where e.Id = Code
              Select e).SingleOrDefault
        Else
            Busqueda = (From e In _context.GlosaObjectionsReceptionD.AsNoTracking
                          Where e.Id = Code
                          Select e).SingleOrDefault
        End If
        If Busqueda IsNot Nothing Then
            Return Busqueda
        Else
            Return New GlosaObjectionsReceptionD
        End If
    End Function


    ''' <summary>
    ''' Retorna un Objeto factura
    ''' </summary>
    ''' <param name="Id">Codigo ID</param>
    ''' <returns>Objeto Factura</returns>
    Public Function getObjectionReceptionDByIdWithoutObjC(Id As String) As GlosaObjectionsReceptionD Implements IObjectionsReceptionDRepository.getObjectionReceptionDByIdWithoutObjC
        Dim Busqueda = (From e In _context.GlosaObjectionsReceptionD.Include("GlosaPortfolioGlosada")
        Where e.Id = Id
          Select e).SingleOrDefault
        If Busqueda IsNot Nothing Then
            Return Busqueda
        Else
            Return New GlosaObjectionsReceptionD
        End If
    End Function



    ''' <summary>
    ''' obtiene el detalle de una recepcion
    ''' </summary>
    ''' <param name="InvoiceNumber">codigo de factura</param>
    '''  <param name="GlosaObjectionsReceptionCId">codigo de cabebcera</param>
    ''' <returns>un objecto detalle de oficio</returns>
    Public Function getObjectionReceptionD(InvoiceNumber As String, GlosaObjectionsReceptionCId As String) As GlosaObjectionsReceptionD Implements IObjectionsReceptionDRepository.getObjectionReceptionD
        Dim Busqueda = From e In _context.GlosaObjectionsReceptionD.Include("GlosaPortfolioGlosada").Include("GlosaObjectionsReceptionC").Include("GlosaObjectionsReceptionC.Customer").Include("GlosasParametersInterface")
                       Where e.GlosaObjectionsReceptionCId = GlosaObjectionsReceptionCId And e.InvoiceNumber = InvoiceNumber
                       Select e
        If Busqueda.Count > 0 Then
            Busqueda.FirstOrDefault.StateRecord = True

            Busqueda.First.OriginalValue = (From a In _context.GlosaObjectionsReceptionD.AsNoTracking
                                                Where a.Id = Busqueda.FirstOrDefault.Id
                                                Select a).FirstOrDefault

            If Busqueda.FirstOrDefault.State = 2 And Busqueda.FirstOrDefault.DocumentType = 2 Then

                'busco el detalle de glosa
                Dim ObjDGlosa = From e In _context.GlosaObjectionsReceptionD
                                                                   Where e.InvoiceNumber = Busqueda.FirstOrDefault.InvoiceNumber And e.DocumentType = 1

                Dim ObjD As New GlosaObjectionsReceptionD
                ObjD = CType(ObjDGlosa.FirstOrDefault, GlosaObjectionsReceptionD)

                'busco el oficio de glosa
                Dim BusquedaObjCGlosa = From e In _context.GlosaObjectionsReceptionC
                                                 Where e.Id = ObjD.GlosaObjectionsReceptionCId

                Dim Objc As New GlosaObjectionsReceptionC
                Objc = CType(BusquedaObjCGlosa.FirstOrDefault, GlosaObjectionsReceptionC)

                'agregamos el oficio a la lsita para que se vea reflejado en la reiteracion
                Dim listC As New List(Of GlosaObjectionsReceptionC)
                listC.Add(Objc)
                Busqueda.First.ListObjectionReceptionC = listC

            End If


            Return Busqueda.First
        Else
            Return New GlosaObjectionsReceptionD
        End If
    End Function

    ''' <summary>
    ''' obtiene una lista de detalles de una recepcion
    ''' </summary>
    ''' <param name="GlosaObjectionsReceptionCId">codigo de la recpcion</param>
    ''' <returns>una lista detalle de oficio</returns>
    Public Function ListObjectionReceptionD(GlosaObjectionsReceptionCId As String) As List(Of GlosaObjectionsReceptionD) Implements IObjectionsReceptionDRepository.ListObjectionReceptionD
        Dim Busqueda = (From e In _context.GlosaObjectionsReceptionD.
                            Include("GlosaPortfolioGlosada").
                            Include("GlosaObjectionsReceptionC").
                            Include("GlosaObjectionsReceptionC.Customer").
                            Include("GlosasParametersInterface")
                        Where e.GlosaObjectionsReceptionCId = GlosaObjectionsReceptionCId).ToList()

        For Each item As GlosaObjectionsReceptionD In Busqueda.ToList
            item.StateRecord = True

            If item.State = 2 And item.DocumentType = 2 Then
                'busco el detalle de glosa
                Dim ObjDGlosa = From e In _context.GlosaObjectionsReceptionD Where e.InvoiceNumber = item.InvoiceNumber And e.DocumentType = 1

                Dim ObjD = CType(ObjDGlosa.FirstOrDefault, GlosaObjectionsReceptionD)

                'busco el oficio de glosa
                Dim BusquedaObjCGlosa = From e In _context.GlosaObjectionsReceptionC Where e.Id = ObjD.GlosaObjectionsReceptionCId

                Dim Objc = CType(BusquedaObjCGlosa.FirstOrDefault, GlosaObjectionsReceptionC)

                'agregamos el oficio a la lsita para que se vea reflejado en la reiteracion
                item.ListObjectionReceptionC = New List(Of GlosaObjectionsReceptionC) From {Objc}
            End If

            If item.RadicateResponsibleId IsNot Nothing Then
                Dim responsible = (From r In _context.Responsible.AsNoTracking() Where r.Id = item.RadicateResponsibleId).FirstOrDefault()
                item.RadicateResponsibleCodeName = String.Format("{0} - {1}", responsible.Code, responsible.Name)
            End If

            Dim customerConsecutive = _context.RadicateInvoiceC.Where(Function(o) o.RadicatedConsecutive = item.GlosaPortfolioGlosada.RadicatedNumber).Select(Function(m) m.CustomerRadicateConsecutive).FirstOrDefault()
            item.CustomerRadicateConsecutive = customerConsecutive

            item.OriginalValue = (From a In _context.GlosaObjectionsReceptionD.AsNoTracking Where a.Id = item.Id Select a).FirstOrDefault
        Next
        Return Busqueda.ToList()
    End Function

    ''' <summary>
    ''' obtiene una lista de detalles, cargando la cartera glosada, detalles quirugicos
    ''' </summary>
    ''' <param name="GlosaObjectionsReceptionDId">codigo del detalle a buscar</param>
    ''' <returns>una lista detalle de oficio</returns>
    Public Function GetListObjectionReceptionD(GlosaObjectionsReceptionDId As String) As List(Of GlosaObjectionsReceptionD) Implements IObjectionsReceptionDRepository.GetListObjectionReceptionD
        Dim Busqueda = (From e In _context.GlosaObjectionsReceptionD.Include("GlosaObjectionsReceptionC").Include("GlosaPortfolioGlosada").Include("GlosaInvoiceDetail").Include("GlosaInvoiceDetail.GlosaInvoiceDetailQX").Include("GlosasParametersInterface")
               Select e
                Where e.Id = GlosaObjectionsReceptionDId).ToList()

        Return Busqueda.ToList()
    End Function

    ''' <summary>
    ''' obtiene una lista de detalles confirmados de una recepcion 
    ''' </summary>
    ''' <param name="Nit">Nit de la objecion</param>
    ''' <returns>una lista detalle de oficio</returns>
    Public Function ListConfirmObjectionReceptionD(Nit As String) As List(Of GlosaObjectionsReceptionD) Implements IObjectionsReceptionDRepository.ListConfirmObjectionReceptionD

        Dim Busqueda = (From e In _context.GlosaObjectionsReceptionD.Include("GlosaPortfolioGlosada").Include("GlosasParametersInterface")
               Select e
                Where e.GlosaObjectionsReceptionC.Customer.Nit = Nit And e.GlosaObjectionsReceptionC.State = 2)

        Return Busqueda.ToList()
    End Function



    ''' <summary>
    ''' Obtiene una lista de detalles confirmados de una recepcion segun un responsable
    ''' </summary>
    ''' <param name="CodeResponsable">Id Responsable</param>
    ''' <returns>una lista detalle de oficio</returns>
    Public Function ListObjectionReceptionDByResponsable(CodeResponsable As String) As List(Of GlosaObjectionsReceptionD) Implements IObjectionsReceptionDRepository.ListObjectionReceptionDByResponsable
        Dim ListObjects = (From e In _context.GlosaObjectionsReceptionD.Include("GlosaObjectionsReceptionC").Include("GlosaObjectionsReceptionC.Customer").Include("GlosaPortfolioGlosada").Include("GlosaInvoiceDetail").Include("GlosaInvoiceDetail.GlosaMovementGlosa").Include("GlosaInvoiceDetail.GlosaMovementGlosa.Responsible1").Include("GlosaInvoiceDetail.GlosaMovementGlosa.Responsible")
                    Where ((e.GlosaPortfolioGlosada.State = "2" And e.DocumentType = "1") Or (e.GlosaPortfolioGlosada.State = "5" And e.DocumentType = "2")) _
        Select e).ToList
        Dim listObjections As New List(Of GlosaObjectionsReceptionD)
        For Each item As GlosaObjectionsReceptionD In ListObjects
            If item.DocumentType = "1" Then
                Dim result = (From e In ListObjects
                             Where e.Id = item.Id AndAlso ((e.GlosaPortfolioGlosada.State = "2" And e.DocumentType = "1") Or (e.GlosaPortfolioGlosada.State = "5" And e.DocumentType = "2")) _
                             AndAlso e.GlosaInvoiceDetail.Any(Function(c) c.GlosaMovementGlosa.Any(Function(a) (a.State <> "2" AndAlso a.State <> "4") AndAlso (a.Responsible1 IsNot Nothing AndAlso a.Responsible1.CodeUser = CodeResponsable) OrElse (a.Responsible IsNot Nothing AndAlso a.Responsible.CodeUser = CodeResponsable)) = True)
                             Select e).SingleOrDefault
                If result IsNot Nothing Then
                    listObjections.Add(result)
                End If

            ElseIf item.DocumentType = "2" Then
                Dim invoiceDetail = (From a In _context.GlosaInvoiceDetail.Include("GlosaMovementGlosa").Include("GlosaMovementGlosa.Responsible1").Include("GlosaMovementGlosa.Responsible")
                                     Where a.InvoiceNumber = item.InvoiceNumber).ToList
                Dim listInvoices As New List(Of GlosaInvoiceDetail)
                For Each itemIDetail In invoiceDetail
                    Dim total = itemIDetail.GlosaMovementGlosa.Where(Function(d) d.State <> "2" AndAlso d.State <> "4").ToList
                    Dim flagMov = 0
                    If total IsNot Nothing AndAlso total.Count > 0 Then
                        Dim totalRes = total.Where(Function(d) (d.Responsible1 IsNot Nothing AndAlso d.Responsible1.CodeUser = CodeResponsable) OrElse (d.Responsible IsNot Nothing AndAlso d.Responsible.CodeUser = CodeResponsable))
                        If totalRes IsNot Nothing AndAlso totalRes.Count > 0 Then
                            flagMov = 1
                        End If
                    End If
                    If flagMov = 1 Then
                        listInvoices.Add(itemIDetail)
                    End If
                Next
                'invoiceDetail = listInvoices
                'AndAlso _
                'a.GlosaMovementGlosa.Any(Function(d) ((d.State <> "2" AndAlso d.State <> "4")) AndAlso (d.Responsible1 IsNot Nothing AndAlso d.Responsible1.CodeERP = CodeResponsable) OrElse (d.Responsible IsNot Nothing AndAlso d.Responsible.CodeERP = CodeResponsable)) = True _
                'Select a).ToList
                If listInvoices.Count > 0 Then
                    Dim ListTrack As New TrackableCollection(Of GlosaInvoiceDetail)
                    For Each itemAux In invoiceDetail
                        ListTrack.Add(itemAux)
                    Next
                    item.GlosaInvoiceDetail = ListTrack
                    listObjections.Add(item)
                End If
            End If
        Next

        Return listObjections

    End Function

    ''' <summary>
    ''' Obtiene un objection D personalizado
    ''' </summary>
    ''' <returns>Objeto</returns>
    Public Function getObjectionDParametersTime(InvoiceNumber As String, type As String) As TrazabilityParametersTime Implements IObjectionsReceptionDRepository.getObjectionDParametersTime
        Dim gloss = (From e In _context.GlosaObjectionsReceptionD.Include("GlosaObjectionsReceptionC").Include("GlosaPortfolioGlosada").Include("GlosaPortfolioGlosada.Responsible").Include("GlosaPortfolioGlosada.Responsible1").Include("GlosaPortfolioGlosada.Responsible2").Include("GlosaPortfolioGlosada.Responsible3")
                     Where e.InvoiceNumber = InvoiceNumber And e.DocumentType = type
                     Select e).FirstOrDefault
        If gloss IsNot Nothing AndAlso gloss.Id > 0 Then
            Dim trazability As TrazabilityParametersTime = New TrazabilityParametersTime With {.Id = gloss.Id, .DocumentDate = gloss.GlosaObjectionsReceptionC.DocumentDate,
                                                .DateResponseDocument = gloss.GlosaObjectionsReceptionC.DateResponsePostDocument,
                                                .ConfirmerUser = gloss.GlosaObjectionsReceptionC.ConfirmUser, .GlosaPortFolio = gloss.GlosaPortfolioGlosada,
                                                .CompleteDate = gloss.GlosaObjectionsReceptionC.ConfirmDate}
            Return trazability
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene un objection D personalizado
    ''' </summary>
    ''' <returns>Objeto</returns>
    Public Function getListObjectionDParametersTime(InvoiceNumbers As String(), type As String) As List(Of TrazabilityParametersTime) Implements IObjectionsReceptionDRepository.getListObjectionDParametersTime
        Dim listTrazability As New List(Of TrazabilityParametersTime)
        Dim listGlosas = (From e In _context.GlosaObjectionsReceptionD.AsNoTracking().
                              Include("GlosaObjectionsReceptionC").AsNoTracking().
                              Include("GlosaPortfolioGlosada").AsNoTracking().
                              Include("GlosaPortfolioGlosada.Responsible").AsNoTracking().
                              Include("GlosaPortfolioGlosada.Responsible1").AsNoTracking().
                              Include("GlosaPortfolioGlosada.Responsible2").AsNoTracking().
                              Include("GlosaPortfolioGlosada.Responsible3").AsNoTracking()
                          Where InvoiceNumbers.Contains(e.InvoiceNumber) AndAlso e.DocumentType = type Select e).ToList()
        If listGlosas IsNot Nothing AndAlso listGlosas.Any Then
            For Each group In listGlosas.GroupBy(Function(gord) gord.InvoiceNumber)
                Dim gloss = listGlosas.Where(Function(gord) gord.InvoiceNumber = group.Key).FirstOrDefault()
                listTrazability.Add(New TrazabilityParametersTime With {
                    .Id = gloss.Id,
                    .InvoiceNumber = gloss.InvoiceNumber,
                    .DocumentDate = gloss.GlosaObjectionsReceptionC.DocumentDate,
                    .DateResponseDocument = gloss.GlosaObjectionsReceptionC.DateResponsePostDocument,
                    .ConfirmerUser = gloss.GlosaObjectionsReceptionC.ConfirmUser,
                    .CompleteDate = gloss.GlosaObjectionsReceptionC.ConfirmDate,
                    .GlosaPortFolio = gloss.GlosaPortfolioGlosada
                })
            Next
        End If
        Return listTrazability
    End Function

    ''' <summary>
    ''' Objeto factura con agregado de parametros contable
    ''' </summary>
    ''' <param name="InvoiceNumber">numero de factura</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetObjDWithInterface(InvoiceNumber As String) As GlosaObjectionsReceptionD Implements IObjectionsReceptionDRepository.GetObjDWithInterface
        Dim Busqueda = (From e In _context.GlosaObjectionsReceptionD.Include("GlosasParametersInterface")
               Select e
                Where e.InvoiceNumber = InvoiceNumber And e.DocumentType = 1)
        If Busqueda.Count > 0 Then
            Return Busqueda.Single
        Else
            Return New GlosaObjectionsReceptionD
        End If
    End Function

End Class
