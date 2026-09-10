'***********************************************************************
' Assembly         : Application.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 25-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Queue
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class ThirdPartyAdminService
    Implements IThirdPartyAdminService


    ' Repositoria de Terceros
    Private _ThirdPartyRepository As IThirdPartyRepository
    ' Repositorio de personas
    Private _PersonRepository As IPersonRepository

    ''' <summary>
    ''' Repositorio para la sucursal
    ''' </summary>
    Private _branchOfficeRepository As IBranchOfficeRepository

    ''' <summary>
    ''' repositorio de BasicBilling
    ''' </summary>
    Private _basicBillingRepository As IBasicBillingRepository

    ''' <summary>
    ''' Fabrica de Indiigo Queue
    ''' </summary>
    Private _factoryQueue As IFactoryQueue

    ''' <summary>
    ''' Costructor el cual inicia el repositorio de terceros
    ''' </summary>
    ''' <param name="repository">Repositorio de terceros</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal repository As IThirdPartyRepository, ByVal personRepository As IPersonRepository, branchOfficeRepository As IBranchOfficeRepository,
                   ByVal BasicBillingRepository As IBasicBillingRepository, FactoryQueue As IFactoryQueue)
        If (repository Is Nothing) Or (personRepository Is Nothing) Or (branchOfficeRepository Is Nothing) Then
            Throw New ArgumentNullException("repository Vacio")
        End If
        _basicBillingRepository = BasicBillingRepository
        _ThirdPartyRepository = repository
        _PersonRepository = personRepository
        _branchOfficeRepository = branchOfficeRepository
        _factoryQueue = FactoryQueue
    End Sub

    ''' <summary>

    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteThirdParty(thirdParty As Domain.Entities.ThirdParty, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IThirdPartyAdminService.DeleteThirdParty
        If thirdParty Is Nothing Then
            Throw New ArgumentNullException("Tercero vacio")
        End If

        Dim unitWork As IUnitWork = _ThirdPartyRepository.UnitWork
        Dim uniWorkPerson As IUnitWork = _PersonRepository.UnitWork
        CType(unitWork, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim resultStored = _PersonRepository.SP_DeleteThirdParty(thirdParty.Nit)
                If resultStored.CodeMessage <> 0 Then
                    uniWorkPerson.RollbackChanges()
                    unitWork.RollbackChanges()
                    scope.Dispose()
                    Return False
                End If

                unitWork.Commit()
                uniWorkPerson.Commit()

                thirdParty.MarkAsDeleted()
                TriggerEvent(thirdParty, audit)

                scope.Complete()
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(thirdParty.GetType.Name, audit.Functional, thirdParty.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                '/*****Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.ThirdParty)(thirdParty, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                auditObject.Execute()

                Return True
            End Using
        Catch ex As Exception
            unitWork.RollbackChanges()
            uniWorkPerson.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Busca un tercero atraves de su nit
    ''' </summary>
    ''' <param name="nit">Nit del tercero</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetThirdPartyByNit(nit As String) As Domain.Entities.ThirdParty Implements IThirdPartyAdminService.GetThirdPartyByNit
        If String.IsNullOrEmpty(nit) Then
            Throw New ArgumentNullException("Nit vacio")
        End If
        Try
            Dim thirdParty = _ThirdPartyRepository.GetThirdPartyByNit(nit)
            If thirdParty.ThirdPartyBranchOffice IsNot Nothing AndAlso thirdParty.ThirdPartyBranchOffice.Count > 0 Then
                For Each item In thirdParty.ThirdPartyBranchOffice.ToList()
                    Dim info = _branchOfficeRepository.GetBranchOfficeById(item.BranchOfficeId)
                    item.BranchOfficeDescription = info.Code + " - " + info.Name
                Next
            End If
            Return thirdParty
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Domain.Entities.ThirdParty()
        End Try
    End Function

    Public Function GetThirdPartyByNitWithAgregates(nit As String) As Domain.Entities.ThirdParty Implements IThirdPartyAdminService.GetThirdPartyByNitWithAgregates
        If String.IsNullOrEmpty(nit) Then
            Throw New ArgumentNullException("Nit vacio")
        End If
        Try
            Dim thirdParty = _ThirdPartyRepository.GetThirdPartyByNitWithAgregates(nit)
            Return thirdParty
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Domain.Entities.ThirdParty()
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el tipo de telefono
    ''' </summary>
    ''' <returns></returns>
    Public Function GetPhoneType() As PhoneType Implements IThirdPartyAdminService.GetPhoneType
        Return _ThirdPartyRepository.GetPhoneType()
    End Function

    ''' <summary>
    ''' Lista todos los terceros
    ''' </summary>
    ''' <returns>Lista de terceros</returns>
    ''' <remarks></remarks>
    Public Function ListAllThirdParty() As List(Of Domain.Entities.ThirdParty) Implements IThirdPartyAdminService.ListAllThirdParty
        Try
            Return _ThirdPartyRepository.ListAllThirdParty()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Funcion que nos retorna si la longitud del Nit es correcta
    ''' </summary>
    Public Function ValidateLenghtNit(ByVal ThirdPartyNit As String, ByVal IdentificationAcronyms As String) As ActionResult(Of Boolean) Implements IThirdPartyAdminService.ValidateLenghtNit
        Try
            Return _ThirdPartyRepository.ValidateLenghtNit(ThirdPartyNit, IdentificationAcronyms)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o edita el tercero
    ''' </summary>
    ''' <param name="thirdParty">Tercero</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveThirdParty(thirdParty As Domain.Entities.ThirdParty, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IThirdPartyAdminService.SaveThirdParty
        If thirdParty Is Nothing Then
            Throw New ArgumentNullException("Tercero Vacio")
        End If

        Dim unitWork As IUnitWork = _ThirdPartyRepository.UnitWork
        Try
            'Se validara la longitud del Nit solo si es un registro nuevo
            If thirdParty.Person.Id = 0 Then

                If String.IsNullOrEmpty(thirdParty.Person?.DocumentTypeAbbreviation) AndAlso thirdParty.Person?.IdentificationTypeId Is Nothing Then
                    Throw New ArgumentNullException(NameOf(thirdParty.Person.IdentificationTypeId), "Id de la identificación vacia")
                End If

                If String.IsNullOrEmpty(thirdParty.Person?.DocumentTypeAbbreviation) Then
                    thirdParty.Person.DocumentTypeAbbreviation = Me._PersonRepository.ExecuteQuery(Of String)("select top 1 SIGLA from ADTIPOIDENTIFICA WHERE ID ={0}", thirdParty.Person.IdentificationTypeId)?.FirstOrDefault
                End If

                Dim validation = Me.ValidateLenghtNit(thirdParty.Person.IdentificationNumber, thirdParty.Person.DocumentTypeAbbreviation)
                If Not validation.StateResult Then
                    Throw New Exception(validation.Message)
                End If
            End If

            Dim AuxThirdParty As Domain.Entities.ThirdParty
            Dim AuxPerson As Person
            If thirdParty.ChangeTracker.State = ObjectState.Modified Then
                AuxThirdParty = _ThirdPartyRepository.GetThirdPartyByNit(thirdParty.Nit, False)
            End If
            If thirdParty.Person.ChangeTracker.State = ObjectState.Modified Then
                AuxPerson = _PersonRepository.GetPersonByIdentification(thirdParty.Person.IdentificationNumber, False)
            End If

            Dim listThirdPartyAddress = New List(Of Address)
            Dim listThirdPartyEmail = New List(Of Email)
            Dim listThirdPartyPhone = New List(Of Phone)
            Dim FiscalResponsability = New List(Of ThirdPartyFiscalResponsibility)

            'Guardar listas
            If thirdParty.Person IsNot Nothing Then
                If thirdParty.Person.Address IsNot Nothing OrElse thirdParty.Person.Address.Count > 0 Then
                    listThirdPartyAddress = (From Address In thirdParty.Person.Address Select Address).ToList()
                    'validacion para saber si se puede o no eliminar la direccion
                    Dim AdressValidation = listThirdPartyAddress.Where(Function(q) q.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted).ToList()
                    If AdressValidation IsNot Nothing AndAlso AdressValidation.Count > 0 Then
                        AdressValidation.ForEach(Sub(g)
                                                     If _basicBillingRepository.ValidateAdressInBasicBilling(g.Id) Then
                                                         Throw New Exception("No se pueden eliminar la direccion(es) debido a que ya estan vinculada(s) con documentos de facturación basica")
                                                     End If
                                                 End Sub)
                    End If
                End If

                If thirdParty.Person.Email IsNot Nothing OrElse thirdParty.Person.Email.Count > 0 Then
                    listThirdPartyEmail = (From Email In thirdParty.Person.Email Select Email).ToList()
                End If

                If thirdParty.Person.Phone IsNot Nothing OrElse thirdParty.Person.Phone.Count > 0 Then
                    listThirdPartyPhone = (From Phone In thirdParty.Person.Phone Select Phone).ToList()
                End If
            End If

            If thirdParty.ThirdPartyFiscalResponsibility IsNot Nothing OrElse thirdParty.ThirdPartyFiscalResponsibility.Count > 0 Then
                FiscalResponsability = (From fiscal In thirdParty.ThirdPartyFiscalResponsibility Select fiscal).ToList()
            End If

            ''Valido la auditoria de tercero
            Dim MessageResult As String = String.Empty
            Dim auxObjEntity As ThirdParty = Nothing
            Dim status As Integer

            If thirdParty.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                thirdParty.CreationUser = audit.CodeUser
                thirdParty.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                MessageResult = ResourceManager.GetString("UpdateMessage")
                auxObjEntity = thirdParty.OriginalValue
                thirdParty.ModificationUser = audit.CodeUser
                thirdParty.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            If thirdParty.ChangeTracker.State <> ObjectState.Unchanged Then
                _ThirdPartyRepository.SaveEntity(thirdParty)
            ElseIf thirdParty.ChangeTracker.State = ObjectState.Unchanged AndAlso thirdParty.Person.ChangeTracker.State <> ObjectState.Unchanged Then
                _ThirdPartyRepository.SaveEntity(thirdParty)
            End If
            unitWork.Commit()

            'rescatar persona
            thirdParty.Person = _PersonRepository.GetPersonByIdentification(thirdParty.Nit, False)
            If listThirdPartyAddress.Count > 0 Then
                For Each item In listThirdPartyAddress
                    thirdParty.Person.Address.Add(item)
                Next
            End If

            If listThirdPartyEmail.Count > 0 Then
                For Each item In listThirdPartyEmail
                    thirdParty.Person.Email.Add(item)
                Next
            End If

            If listThirdPartyPhone.Count > 0 Then
                For Each item In listThirdPartyPhone
                    thirdParty.Person.Phone.Add(item)
                Next
            End If

            If FiscalResponsability.Count > 0 Then
                For Each item In FiscalResponsability
                    thirdParty.ThirdPartyFiscalResponsibility.Add(item)
                Next
            End If

            TriggerEvent(thirdParty, audit)

            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Busca una persona por el numero de identificacion
    ''' </summary>
    ''' <param name="identificationNumber">Numero de identificacion de la persona</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPersonByIdentification(identificationNumber As String) As Person Implements IThirdPartyAdminService.GetPersonByIdentification
        If String.IsNullOrEmpty(identificationNumber) Then
            Throw New ArgumentNullException("Identificacion vacia")
        End If
        Try
            Return _PersonRepository.GetPersonByIdentification(identificationNumber)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Person()
        End Try
    End Function

    ''' <summary>
    ''' Obtener un tercero por el id
    ''' </summary>
    ''' <param name="idThirdParty"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetThirdPartyById(idThirdParty As Integer, audit As AuditMessage) As Domain.Entities.ThirdParty Implements IThirdPartyAdminService.GetThirdPartyById
        If idThirdParty = 0 Then
            Throw New ArgumentNullException("idThirdParty")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim third As Domain.Entities.ThirdParty = Me._ThirdPartyRepository.GetThirdPartyById(idThirdParty)
            If third IsNot Nothing AndAlso third.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.ThirdParty)(third, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return third
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function UpdateStateThirdParty(id As Integer, state As Boolean, audit As AuditMessage) As Boolean Implements IThirdPartyAdminService.UpdateStateThirdParty
        Try
            Dim third = _ThirdPartyRepository.GetThirdPartyById(id)
            If third IsNot Nothing AndAlso third.Id > 0 Then
                third.State = state
                third.MarkAsModified()
                third.Person.State = state
                third.Person.MarkAsModified()
            End If
            Return Me.SaveThirdParty(third, audit)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function


#Region "Events"

    Public Sub TriggerEvent(thirdParty As ThirdParty, audit As AuditMessage)
        Dim wrapperEvent As New Events.Serializers.Wrapper
        Dim ChangeTracker As String = thirdParty.ChangeTracker.State.ToString().ToLower()

        Dim eventData As EventData = wrapperEvent.GenerateWrapperEventData(thirdParty, audit.CodeUser, ChangeTracker, DittoSourceType.thirdParty)
        Dim Queue As IIndigoQueue = _factoryQueue.CreateQueue()
        Queue.Publish(eventData)
    End Sub

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _ThirdPartyRepository = Nothing
            _PersonRepository = Nothing
            _branchOfficeRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class
