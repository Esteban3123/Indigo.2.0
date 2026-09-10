Imports Domain.Common
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core
Imports Application.Common
Imports System.Text
Imports System.Globalization

Public Class CurrencyAdminService
    Implements ICurrencyAdminService


    Private _CurrencyRepository As ICurrencyRepository
    Private _secuenseDRepository As ISequenseAccountingDRepository
    Private _settingsBillingRepository As ISettingsBillingRepository
    Public Const FORM_NAME As String = "Monedas"

    ''' <summary>
    ''' Initializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="CurrencyRepository">el repositorio para el manejo de los Países.</param>
    Public Sub New(ByVal CurrencyRepository As ICurrencyRepository, secuenseDRepository As ISequenseAccountingDRepository,
                   settingsBillingRepository As ISettingsBillingRepository)
        If CurrencyRepository Is Nothing Then
            Throw New ArgumentNullException("CurrencyRepository Vacio")
        End If
        _CurrencyRepository = CurrencyRepository
        _secuenseDRepository = secuenseDRepository
        _settingsBillingRepository = settingsBillingRepository
    End Sub

    Public Function DeleteCurrency(Currency As Domain.Entities.Currency, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of Currency) Implements ICurrencyAdminService.DeleteCurrency
        Dim result As New ActionMessageResult(Of Currency)
        result.StateResult = True
        If _CurrencyRepository Is Nothing Then
            Throw New ArgumentNullException("CurrencyRepository Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _CurrencyRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Currency.ModificationUser = audit.CodeUser
                Currency.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Currency)(Currency, audit, status)

                Currency.MarkAsDeleted()
                Me._CurrencyRepository.SaveEntity(Currency)
                UnitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()

                result.StatusCode = eStatusResult.SUCCESS
                result.Message = ResourceManager.GetString("RecordDeleted")

                Return result
            End Using
        Catch ex As OptimisticConcurrencyException
            result.StatusCode = eStatusResult.WARNING
            result.Message = ResourceManager.GetString("ErrorConcurrence")
            result.StateResult = False
            UnitOfWork.RollbackChanges()
            Return result
        Catch ex As UpdateException
            result.StatusCode = eStatusResult.WARNING
            result.Message = ResourceManager.GetString("ErrorDependence")
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", Currency.Code))
            UnitOfWork.RollbackChanges()
            Return result
        Catch ex As DbUpdateException
            result.StatusCode = eStatusResult.WARNING
            result.Message = ResourceManager.GetString("ErrorDependence")
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", Currency.Code))
            UnitOfWork.RollbackChanges()
            Return result
        Catch ex As Exception
            result.StatusCode = eStatusResult.EXCEPTION
            result.Message = IndigoManagementExceptions.GetExceptionDetails(ex)
            result.StateResult = False

            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return result
        End Try
    End Function

    Public Function GetCurrency(code As String) As Domain.Entities.Currency Implements ICurrencyAdminService.GetCurrency
        If String.IsNullOrEmpty(code) = True Then
            Throw New ArgumentNullException("codeCurrency vacio")
        End If
        Try
            Return _CurrencyRepository.GetCurrency(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Devuelve un país por ID
    ''' </summary>
    ''' <param name="idCurrency">Id del pais</param>
    ''' <returns>El pais</returns>
    ''' <remarks></remarks>
    Public Function GetCurrencyById(ByVal idCurrency As Integer, ByVal audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Entities.Currency Implements ICurrencyAdminService.GetCurrencyById
        If Not (idCurrency > 0) Then
            Throw New ArgumentNullException("idCurrency Vacio")
        End If
        Try
            Return _CurrencyRepository.GetCurrencyById(idCurrency)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Domain.Entities.Currency
        End Try
    End Function

    Public Function ListAllCurrency() As List(Of Domain.Entities.Currency) Implements ICurrencyAdminService.ListAllCurrency
        Try
            Return _CurrencyRepository.ListAllCurrency()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveCurrency(Currency As Domain.Entities.Currency, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of Currency) Implements ICurrencyAdminService.SaveCurrency
        If Currency Is Nothing Then
            Throw New ArgumentNullException("Currency Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _CurrencyRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            If Currency.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added AndAlso Me._CurrencyRepository.GetByFilter(Function(f) f.ISO4217Id = Currency.ISO4217Id)?.Any() Then
                Return New ActionResult(Of Currency) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = "No se puede agregar el registro porque ya existe uno con el mismo tipo de moneda"}
            End If
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                Dim seq As GeneralLedgerSequenceDetail = Nothing
                If Currency.Code Is Nothing OrElse Currency.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.GeneralLedgerSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            Currency.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of Currency) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.GeneralLedgerSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), Currency.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of Currency) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxCommon As Currency = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of Currency)
                Dim status As Integer

                If Currency.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Currency.CreationUser = audit.CodeUser
                    Currency.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxCommon = Currency.OriginalValue
                    Currency.ModificationUser = audit.CodeUser
                    Currency.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._CurrencyRepository.SaveEntity(Currency)
                UnitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of Currency)(Currency, audit, status, auxCommon)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                Currency.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of Currency) With {.StatusCode = eStatusResult.SUCCESS, .StateResult = True, .ObjectEmbbeded = Currency, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            UnitOfWork.RollbackChanges()
            Return New ActionResult(Of Currency) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Currency) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' consulta la tasa de cambio con respecto a la moneda oficial
    ''' </summary>
    ''' <param name="ToCurrencyId"> id de la moneda a convertir</param>
    ''' <param name="FromCurrencyId">id de la moneda de donde se va convertir</param>
    ''' <param name="session"> variable de session para extraer la moneda oficial</param>
    ''' <returns></returns>
    Public Function GetTRMValuebyCurrencyId(ToCurrencyId As Integer, FromCurrencyId As Integer, session As SessionValues,
                                             Optional DateTrm As Date? = Nothing, Optional entityName As String = Nothing) As ActionResult(Of TRM) Implements ICurrencyAdminService.GetTRMbyCurrencyId
        Try
            If DateTrm Is Nothing Then
                DateTrm = DateTime.Now.Date
            End If

            'se valida si viene un entityname para que se vaya por el flujo nuevo con el custom TRM
            If Not String.IsNullOrEmpty(entityName) Then
                Dim result = GetEspecificTRMByModule(ToCurrencyId, FromCurrencyId, session, DateTrm, entityName)

                If result Is Nothing Then
                    Return New ActionResult(Of TRM) With {.StateResult = False, .Message = "Error en función TRM Custom"}
                End If

                If result?.StateResult OrElse (Not result?.StateResult AndAlso Not result?.StateResultAux) Then
                    Return result
                End If
            End If

            Dim validations = TRMValidations(ToCurrencyId, FromCurrencyId, session, DateTrm)

            If validations Is Nothing OrElse Not validations?.StateResult Then
                Return New ActionResult(Of TRM) With {.StateResult = False, .Message = validations?.Message}
            End If

            If ToCurrencyId = session.OfficialCurrencyId Then
                If (FromCurrencyId = session.OfficialCurrencyId) Then
                    Return New ActionResult(Of TRM) With {.StateResult = True,
                                                  .ObjectEmbbeded = New TRM With {.CurrencyId = ToCurrencyId, .Value = 1, .OfficialCurrencyId = FromCurrencyId, .MeasurementDate = DateTrm},
                                                  .Message = "Consulta Exitosa"}
                End If

                Dim TRM = _CurrencyRepository.GetTRMbyCurrencyId(FromCurrencyId, DateTrm)

                If TRM Is Nothing Then
                    Return New ActionResult(Of TRM) With {.StateResult = False, .Message = "No hay TRM de la moneda incial"}
                End If

                Return New ActionResult(Of TRM) With {.StateResult = True,
                                                  .ObjectEmbbeded = New TRM With {.CurrencyId = ToCurrencyId, .Value = Me.TRMValue(TRM.ValueOfficialToCurrency, TRM.Value), .OfficialCurrencyId = FromCurrencyId, .MeasurementDate = TRM.MeasurementDate},
                                                  .Message = "Consulta Exitosa"}

            End If

            Dim ToCurrencyTRM = _CurrencyRepository.GetTRMbyCurrencyId(ToCurrencyId, DateTrm)

            If ToCurrencyTRM Is Nothing Then
                Return New ActionResult(Of TRM) With {.StateResult = False, .Message = "No hay TRM de la moneda final"}
            End If

            If FromCurrencyId = session.OfficialCurrencyId Then

                Return New ActionResult(Of TRM) With {.StateResult = True,
                                                  .ObjectEmbbeded = New TRM With {.CurrencyId = ToCurrencyId, .Value = Me.TRMValue(ToCurrencyTRM.Value, ToCurrencyTRM.ValueOfficialToCurrency), .OfficialCurrencyId = FromCurrencyId, .MeasurementDate = ToCurrencyTRM.MeasurementDate},
                                                  .Message = "Consulta Exitosa"}
            End If

            Dim FromCurrencyTRM = _CurrencyRepository.GetTRMbyCurrencyId(FromCurrencyId, DateTrm)

            If FromCurrencyTRM Is Nothing Then
                Return New ActionResult(Of TRM) With {.StateResult = False, .Message = "Tasa de cambio de la moneda final vacia"}
            End If

            Dim TRMValue = Me.TRMValue(ToCurrencyTRM.Value, ToCurrencyTRM.ValueOfficialToCurrency) / Me.TRMValue(FromCurrencyTRM.Value, FromCurrencyTRM.ValueOfficialToCurrency)

            Return New ActionResult(Of TRM) With {.StateResult = True,
                                                  .ObjectEmbbeded = New TRM With {.CurrencyId = ToCurrencyId, .Value = TRMValue, .OfficialCurrencyId = FromCurrencyId, .MeasurementDate = FromCurrencyTRM.MeasurementDate},
                                                  .Message = "Consulta Exitosa"}

        Catch ex As Exception
            Return New ActionResult(Of TRM) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' funcion que se encarga de consultar un TRM personalizado por modulo (Billing)
    ''' </summary>
    ''' <param name="ToCurrencyId">Moneda a la que se va convertir respecto a la oficial</param>
    ''' <param name="session">variable de session para obtener la moneda oficial</param>
    ''' <param name="DateTrm">fecha para sacar la tasa </param>
    ''' <param name="entityName">nombre del proceso donde se busca el custom TRM</param>
    ''' <returns></returns>
    Public Function GetEspecificTRMByModule(ToCurrencyId As Integer?, FromCurrencyId As Integer, session As SessionValues,
                                             DateTrm As Date?, entityName As String, Optional OperativeUnitId As Integer? = Nothing) As ActionResult(Of TRM)
        'Se verifica que el entityName no este vacio 
        If String.IsNullOrEmpty(entityName) Then
            Return New ActionResult(Of TRM) With {.StateResult = False, .StateResultAux = False, .Message = "Parámetro del modulo vacio"}
        End If
        'se ejecuta la funcion de validacion para saber si algun parametro no cumple, se envia 1 en el fromcurrency porque aqui es solo respecto a la oficial
        Dim validations = TRMValidations(ToCurrencyId, FromCurrencyId, session, DateTrm)
        If validations Is Nothing OrElse Not validations?.StateResult Then
            Return New ActionResult(Of TRM) With {.StateResult = False, .StateResultAux = False, .Message = validations?.Message}
        End If

        Try
            ' select que determina de donde sacaremos el custoTRM
            Select Case entityName
                Case NameOf(Invoice)
                    'se consultan los parametro de facturacion a futuro se implementara el envio de  la unidad
                    Dim settingsBilling = _settingsBillingRepository.FirstOrDefault(Function(s) (OperativeUnitId Is Nothing OrElse s.IdOperatingUnit = OperativeUnitId) AndAlso s.HasCustomTRM, False)

                    If settingsBilling Is Nothing Then
                        Return New ActionResult(Of TRM) With {.StateResult = False, .StateResultAux = True, .Message = "No se econtró parámetros de facturación"}
                    End If

                    ' si la moneda a convertir es igual a la oficial no busco ni hago conversion
                    If ToCurrencyId = FromCurrencyId Then
                        Return New ActionResult(Of TRM) With {.StateResult = True,
                                                  .ObjectEmbbeded = New TRM With {.CurrencyId = ToCurrencyId, .Value = 1, .OfficialCurrencyId = FromCurrencyId, .MeasurementDate = DateTrm},
                                                  .Message = "Consulta Exitosa"}
                    End If

                    Dim tRM As TRM
                    Dim tRMValue As Decimal
                    'busco en la tabla de trm custom 
                    tRM = _CurrencyRepository.GetEspecificModuleTMR(If(ToCurrencyId = session.OfficialCurrencyId, FromCurrencyId, ToCurrencyId), DateTrm)

                    ' si no existe envio mensaje de error
                    If tRM Is Nothing Then
                        Return New ActionResult(Of TRM) With {.StateResult = False, .StateResultAux = False, .Message = "No hay tasa representativa para la fecha o moneda indicada"}
                    End If

                    If ToCurrencyId = session.OfficialCurrencyId Then

                        tRMValue = Me.TRMValue(tRM.ValueOfficialToCurrency, tRM.Value)
                    Else
                        tRMValue = Me.TRMValue(tRM.Value, tRM.ValueOfficialToCurrency)
                    End If

                    'retorno el objeto interfazado con la funcion de trm utilizada en el ERP
                    Return New ActionResult(Of TRM) With {.StateResult = True,
                                                  .ObjectEmbbeded = New TRM With {.CurrencyId = ToCurrencyId, .Value = tRMValue, .OfficialCurrencyId = FromCurrencyId, .MeasurementDate = tRM?.MeasurementDate},
                                                  .Message = "Consulta Exitosa"}
                Case Else
                    Return New ActionResult(Of TRM) With {.StateResult = False, .StateResultAux = True, .Message = $"EL modulo {entityName} no tiene configurado un TRM específico"}
            End Select
        Catch ex As Exception
            Return New ActionResult(Of TRM) With {.StateResult = False, .StateResultAux = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns>False- cuando algo fallo en la validacion 
    '''           true - cuando todo esta ok y no entra en ninguna validacion</returns>
    Private Function TRMValidations(ToCurrencyId As Integer?, FromCurrencyId As Integer?, session As SessionValues,
                                             DateTrm As Date?) As ActionResult
        Dim stringBuilder = New StringBuilder

        If ToCurrencyId Is Nothing OrElse ToCurrencyId = 0 Then
            stringBuilder.AppendLine("Id de la moneda a convertir vacía")
        End If

        If FromCurrencyId Is Nothing OrElse FromCurrencyId = 0 Then
            stringBuilder.AppendLine("Id de la moneda inicial vacía")
        End If

        If session Is Nothing OrElse session?.OfficialCurrencyId = 0 Then
            stringBuilder.AppendLine("Variable de session vacia o Id de la moneda oficial vacía")
        End If

        If DateTrm Is Nothing Then
            stringBuilder.AppendLine("Fecha para la tasa representativa está vacía")
        End If

        Return New ActionResult With {.StateResult = Not stringBuilder.Length > 0, .Message = stringBuilder.ToString}
    End Function

    ''' <summary>
    ''' funcion para saber cual trm elegir en funcion de la necesidad del calculo
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <param name="ReverseValue"></param>
    ''' <returns></returns>
    Private Function TRMValue(Value As Decimal, ReverseValue As Decimal) As Decimal
        If Value >= ReverseValue Then
            Return Value
        Else
            Return (1.0 / ReverseValue)
        End If
    End Function
#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _CurrencyRepository = Nothing
            _secuenseDRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Public Function UpdateCurrency(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Currency) Implements ICurrencyAdminService.UpdateCurrency
        'Throw New NotImplementedException()

        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Try
            Dim Cur As Currency = Me._CurrencyRepository.GetCurrency(code.Trim())
            If Cur IsNot Nothing AndAlso Cur.Id > 0 Then
                Cur.State = state
                Cur.MarkAsModified()
            End If
            Return Me.SaveCurrency(Cur, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Currency) With {.StateResult = False}
        End Try
    End Function
#End Region

End Class
