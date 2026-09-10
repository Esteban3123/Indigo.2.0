'************************************************************
' Assembly         : Infraestructure.Data.GlosasRepository
' Author           : Juan Diego Diaz
' Created          : 24-06-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Repositorio Parametros tiempo
''' </summary>
Public Class TimeParametersRepository
    Inherits GenericRepository(Of TimeParameters)
    Implements ITimeParametersRepository


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
    ''' Obtiene el registro por defecto de Parametros glosas
    ''' </summary>
    ''' <returns>Objeto Parametros Tiempo</returns>
    Public Function GetTimeParametersDefault(ByVal _IdIOperatingUnit As Integer) As TimeParameters Implements ITimeParametersRepository.GetTimeParametersDefault
        Dim timeParameters = From e In _context.TimeParameters
                             Where e.IdCustomer Is Nothing And e.IdOperatingUnit = _IdIOperatingUnit
                             Select e
        If timeParameters.Count > 0 Then
            Return timeParameters.SingleOrDefault()
        Else
            Return New TimeParameters
        End If
    End Function

    ''' <summary>
    ''' Obtiene un registro de Parametros Tiempo especifico.
    ''' </summary>
    ''' <param name="Id">El Id del registro Parametros Tiempo</param>
    ''' <returns>Objeto Parametros Tiempo</returns>
    Public Function GetTimeParameters(Id As String) As TimeParameters Implements ITimeParametersRepository.GetTimeParameters

        Dim timeParameters = From e In _context.TimeParameters
                             Where e.Id = CInt(Id)
                             Select e
        If timeParameters.Count > 0 Then
            Dim timeParametersData = timeParameters.SingleOrDefault
            timeParametersData.OriginalValue = (From e In _context.TimeParameters.AsNoTracking
            Where e.Id = CInt(Id)
            Select e).SingleOrDefault
            Return timeParametersData
        End If
        Return New TimeParameters
    End Function

    ''' <summary>
    ''' Obtiene el primer o unico parametro de tiempo
    ''' </summary>
    ''' <returns>Objeto Parametros Tiempo</returns>
    Public Function GetTimeParametersSingleOrDefault(ByVal _IdIOperatingUnit As Integer, Entity As String) As TimeParameters Implements ITimeParametersRepository.GetTimeParametersSingleOrDefault

        Dim res As TimeParameters
        If Entity = "0" Then
            res = (From e In _context.TimeParameters
                        Where e.IdCustomer Is Nothing And e.IdOperatingUnit = _IdIOperatingUnit
                        Select e).FirstOrDefault
            If res IsNot Nothing Then
                res.OriginalValue = (From e In _context.TimeParameters.AsNoTracking
                            Where e.IdCustomer Is Nothing And e.IdOperatingUnit = _IdIOperatingUnit
                            Select e).SingleOrDefault
            End If

        Else
            res = (From e In _context.TimeParameters
                        Where e.IdCustomer = Entity And e.IdOperatingUnit = _IdIOperatingUnit
                        Select e).FirstOrDefault
            If res IsNot Nothing Then
                res.OriginalValue = (From e In _context.TimeParameters.AsNoTracking
           Where e.IdCustomer = Entity And e.IdOperatingUnit = _IdIOperatingUnit
           Select e).SingleOrDefault
            End If
        End If
        If res IsNot Nothing Then
            Dim conceptNote As PortfolioNoteConcept
            If res.GeneralGlossConceptNoteId IsNot Nothing Then
                conceptNote = (From cn In _context.PortfolioNoteConcept.AsNoTracking Where cn.Id = res.GeneralGlossConceptNoteId Select cn).FirstOrDefault
                res.GeneralGlossConceptNoteDescription = conceptNote.Code + " - " + conceptNote.Name
            End If
            If res.DetailedGlossConceptNoteId IsNot Nothing Then
                conceptNote = (From cn In _context.PortfolioNoteConcept.AsNoTracking Where cn.Id = res.DetailedGlossConceptNoteId Select cn).FirstOrDefault
                res.DetailedGlossConceptNoteDescription = conceptNote.Code + " - " + conceptNote.Name
            End If
            If res.PreviousLifetimesConceptNoteId IsNot Nothing Then
                conceptNote = (From cn In _context.PortfolioNoteConcept.AsNoTracking Where cn.Id = res.PreviousLifetimesConceptNoteId Select cn).FirstOrDefault
                res.PreviousLifetimesConceptNoteDescription = conceptNote.Code + " - " + conceptNote.Name
            End If

            Dim journarlVoucherType As JournalVoucherTypes
            If res.RadicationJournalVoucherTypeId IsNot Nothing Then
                journarlVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.RadicationJournalVoucherTypeId Select jvt).FirstOrDefault
                res.RadicationJournalVoucherTypeDescription = journarlVoucherType.Code + " - " + journarlVoucherType.Name
            End If

            If res.ReceptionObjectionJournalVoucherTypeId IsNot Nothing Then
                journarlVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.ReceptionObjectionJournalVoucherTypeId Select jvt).FirstOrDefault
                res.ReceptionObjectionJournalVoucherTypeDescription = journarlVoucherType.Code + " - " + journarlVoucherType.Name
            End If

            If res.ConciliationJournalVoucherTypeId IsNot Nothing Then
                journarlVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.ConciliationJournalVoucherTypeId Select jvt).FirstOrDefault
                res.ConciliationJournalVoucherTypeDescription = journarlVoucherType.Code + " - " + journarlVoucherType.Name
            End If

            If res.DevolutionJournalVoucherTypeId IsNot Nothing Then
                journarlVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.DevolutionJournalVoucherTypeId Select jvt).FirstOrDefault
                res.DevolutionJournalVoucherTypeDescription = journarlVoucherType.Code + " - " + journarlVoucherType.Name
            End If

            If res.TransferLegalJournalVoucherTypeId IsNot Nothing Then
                journarlVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.TransferLegalJournalVoucherTypeId Select jvt).FirstOrDefault
                res.TransferLegalJournalVoucherTypeDescription = journarlVoucherType.Code + " - " + journarlVoucherType.Name
            End If

            Dim Third As ThirdParty
            If res.PreviousLifetimesThirdPartyId IsNot Nothing Then
                Third = (From t In _context.ThirdParty Where t.Id = res.PreviousLifetimesThirdPartyId).FirstOrDefault
                res.CodeNamethird = Third.Nit + " - " + Third.Name
            End If

            Dim CostCenter As CostCenter
            If res.PreviousLifetimesCostCenterId IsNot Nothing Then
                CostCenter = (From c In _context.CostCenter Where c.Id = res.PreviousLifetimesCostCenterId).FirstOrDefault
                res.CodeNameCostCenter = CostCenter.Code + " - " + CostCenter.Name
            End If

            Return res
        Else
            Return New TimeParameters
        End If

    End Function

    ''' <summary>
    ''' Lista todos los registros de Parametros Tiempo.
    ''' </summary>
    ''' <returns>Lista de objetos de Parametros Tiempo</returns>
    Public Function ListAllTimeParameters(ByVal _IdIOperatingUnit As Integer, Optional Control As String = "") As List(Of TimeParameters) Implements ITimeParametersRepository.ListAllTimeParameters
        If Control = "" Then
            Dim timeParameters = From e In _context.TimeParameters
                                 Where e.IdOperatingUnit = _IdIOperatingUnit
                     Select e
            Return timeParameters.ToList
        Else
            Dim timeParameters = (From e In _context.TimeParameters.Include("Customer")
                    Where e.IdCustomer IsNot Nothing And e.IdOperatingUnit = _IdIOperatingUnit
                    Select e).ToList

            For Each item As TimeParameters In timeParameters
                item.OriginalValue = (From e In _context.TimeParameters.AsNoTracking.Include("Customer").AsNoTracking
                    Where e.Id = item.Id
                    Select e).SingleOrDefault
            Next
            Return timeParameters
        End If
    End Function


    ''' <summary>
    ''' Obtiene una lista de registros para mostrar los limites de tiempos 
    ''' en cada etapa del proceso de glosas
    ''' </summary>
    ''' <param name="Invoice">Numero Factura</param>
    Public Function ListControlParametersTime(Invoice As String, Entity As String, ByVal _IdIOperatingUnit As Integer) As List(Of ControlParametersTime) Implements ITimeParametersRepository.ListControlParametersTime
        'Dim time = (From c In _context.TimeParameters
        'Select c).FirstOrDefault
        Dim time = GetTimeParametersSingleOrDefault(_IdIOperatingUnit, Entity)
        If time.Id = 0 Then
            time = GetTimeParametersSingleOrDefault(_IdIOperatingUnit, "0")
        End If

        Dim listControlTime As List(Of ControlParametersTime) = New List(Of ControlParametersTime)
        Dim control As ControlParametersTime

        If time IsNot Nothing Then

            Dim ObjetionDGlosa = (From e In _context.GlosaObjectionsReceptionD.Include("GlosaObjectionsReceptionC").Include("GlosaPortfolioGlosada").Include("GlosaPortfolioGlosada.Responsible").Include("GlosaPortfolioGlosada.Responsible1")
                Where e.InvoiceNumber = Invoice And e.DocumentType = "1"
                Select e.GlosaObjectionsReceptionC.DocumentDate, e.GlosaObjectionsReceptionC.DateResponsePostDocument, _
                e.GlosaObjectionsReceptionC.ConfirmUser, e.GlosaPortfolioGlosada, e.GlosaObjectionsReceptionC.ConfirmDate, _
                time.MaxTimeResponse, time.MaxTimeExtemporaneousGlosa, time.MaxTimeSendingDocumentResponse).SingleOrDefault

            If ObjetionDGlosa IsNot Nothing Then
                control = New ControlParametersTime With {.Code = "01", .OperationDate = ObjetionDGlosa.DocumentDate, .LimitDate = DateAdd(DateInterval.DayOfYear, ObjetionDGlosa.MaxTimeExtemporaneousGlosa, ObjetionDGlosa.DocumentDate), .CompleteDate = ObjetionDGlosa.ConfirmDate, .TimeParameters = ObjetionDGlosa.MaxTimeExtemporaneousGlosa, .RemainingTime = IIf((.LimitDate - Date.Now).TotalDays < 0, 0, (.LimitDate - Date.Now).TotalDays), _
                                                          .SpendTime = IIf(.CompleteDate Is Nothing, 0, (.CompleteDate - .OperationDate).Value.TotalDays)}
                listControlTime.Add(control)
                Dim responsibleEvaluationGlosa = ObjetionDGlosa.GlosaPortfolioGlosada.Responsible.CodeUser + " - " + ObjetionDGlosa.GlosaPortfolioGlosada.Responsible.Name

                control = New ControlParametersTime With {.Code = "02", .OperationDate = ObjetionDGlosa.DocumentDate, .LimitDate = DateAdd(DateInterval.DayOfYear, ObjetionDGlosa.MaxTimeResponse, ObjetionDGlosa.DocumentDate), .CompleteDate = ObjetionDGlosa.GlosaPortfolioGlosada.EvaluationDateGlosa, .TimeParameters = ObjetionDGlosa.MaxTimeResponse, .RemainingTime = IIf((.LimitDate - Date.Now).TotalDays < 0, 0, (.LimitDate - Date.Now).TotalDays)}
                listControlTime.Add(control)
                Dim responsibleEvaluationReiteration = ObjetionDGlosa.GlosaPortfolioGlosada.Responsible1.CodeUser + " - " + ObjetionDGlosa.GlosaPortfolioGlosada.Responsible1.Name

                control = New ControlParametersTime With {.Code = "03", .OperationDate = ObjetionDGlosa.DocumentDate, .LimitDate = DateAdd(DateInterval.DayOfYear, ObjetionDGlosa.MaxTimeSendingDocumentResponse, ObjetionDGlosa.DocumentDate), .ResponsibleNameCode = responsibleEvaluationReiteration, .TimeParameters = ObjetionDGlosa.MaxTimeSendingDocumentResponse, .RemainingTime = IIf((.LimitDate - Date.Now).TotalDays < 0, 0, (.LimitDate - Date.Now).TotalDays)}
                listControlTime.Add(control)
            End If


            Dim reiteration = (From e In _context.GlosaObjectionsReceptionD.Include("GlosaObjectionsReceptionC").Include("GlosaPortfolioGlosada").Include("GlosaPortfolioGlosada.Responsible2").Include("GlosaPortfolioGlosada.Responsible3")
                        Where e.InvoiceNumber = Invoice And e.DocumentType = "2"
                        Select e.GlosaObjectionsReceptionC.DocumentDate, e.GlosaObjectionsReceptionC.DateResponsePostDocument, e.GlosaPortfolioGlosada, e.GlosaObjectionsReceptionC.ConfirmDate, _
                        time.MaxTimeExtemporaneousReiteration, time.MaxTimeSendingReiterationDocumentResponse, time.MaxTimeReiterationResponse).SingleOrDefault

            If reiteration IsNot Nothing Then
                control = New ControlParametersTime With {.Code = "04", .OperationDate = reiteration.DocumentDate, .LimitDate = DateAdd(DateInterval.DayOfYear, reiteration.MaxTimeExtemporaneousReiteration, reiteration.DocumentDate), .CompleteDate = reiteration.ConfirmDate, .TimeParameters = reiteration.MaxTimeExtemporaneousReiteration, .RemainingTime = IIf((.LimitDate - Date.Now).TotalDays < 0, 0, (.LimitDate - Date.Now).TotalDays), _
                                                          .SpendTime = IIf(.CompleteDate Is Nothing, 0, (.CompleteDate - .OperationDate).Value.TotalDays)}
                listControlTime.Add(control)
                Dim responsibleEvaluationGlosa = reiteration.GlosaPortfolioGlosada.Responsible2.CodeUser + " - " + reiteration.GlosaPortfolioGlosada.Responsible2.Name

                control = New ControlParametersTime With {.Code = "05", .OperationDate = reiteration.DocumentDate, .LimitDate = DateAdd(DateInterval.DayOfYear, reiteration.MaxTimeReiterationResponse, reiteration.DocumentDate), .CompleteDate = reiteration.GlosaPortfolioGlosada.EvaluationDateGlosa, .TimeParameters = reiteration.MaxTimeReiterationResponse, .RemainingTime = IIf((.LimitDate - Date.Now).TotalDays < 0, 0, (.LimitDate - Date.Now).TotalDays)}
                listControlTime.Add(control)
                Dim responsibleEvaluationReiteration = reiteration.GlosaPortfolioGlosada.Responsible3.CodeUser + " - " + reiteration.GlosaPortfolioGlosada.Responsible3.Name

                control = New ControlParametersTime With {.Code = "06", .OperationDate = reiteration.DocumentDate, .LimitDate = DateAdd(DateInterval.DayOfYear, reiteration.MaxTimeSendingReiterationDocumentResponse, reiteration.DocumentDate), .ResponsibleNameCode = responsibleEvaluationReiteration, .TimeParameters = reiteration.MaxTimeSendingReiterationDocumentResponse, .RemainingTime = IIf((.LimitDate - Date.Now).TotalDays < 0, 0, (.LimitDate - Date.Now).TotalDays)}
                listControlTime.Add(control)
            End If

            Dim conciliation = (From e In _context.ConciliationD.Include("ConciliationC")
                        Where e.InvoiceNumber = Invoice
                        Select e.ConciliationC.DocumentDate, e.ConciliationC.ConfirmDate, _
                        time.MaxTimeConciliation).SingleOrDefault
            If (conciliation IsNot Nothing) Then
                control = New ControlParametersTime With {.Code = "07", .OperationDate = conciliation.DocumentDate, .CompleteDate = .CompleteDate, .LimitDate = DateAdd(DateInterval.DayOfYear, conciliation.MaxTimeConciliation, conciliation.DocumentDate), .TimeParameters = conciliation.MaxTimeConciliation, .RemainingTime = (Date.Now - .OperationDate).TotalDays}
                listControlTime.Add(control)
            End If
        Else
            control = New ControlParametersTime
            listControlTime.Add(control)
        End If
        Return listControlTime
    End Function



    Public Function GeneratePoortfolioReclasification(operatingUnitId As Integer, radicateInvoiceId As Integer, codeUser As String) As Domain.Base.Entities.ActionResult Implements ITimeParametersRepository.GeneratePoortfolioReclasification
        Dim result = _context.SP_GeneratePortfolioReclasification(operatingUnitId, radicateInvoiceId, codeUser).FirstOrDefault()
        If result.CodeMessage = 0 Then
            Return New ActionResult With {.StateResult = True, .Message = result.MessageResult}
        Else
            Return New ActionResult With {.StateResult = False, .Message = result.MessageResult}
        End If
    End Function
End Class
