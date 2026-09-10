'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Kevin Garay Rodrgiuez
' Created          : 07-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll
Imports Domain.Common
Imports Domain.Payroll.Entities
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports Application.Payroll
Imports System.IO
Imports System.Xml

Public Class ManualConceptsAdminService
    Implements IManualConceptsAdminService

    ''' <summary>
    ''' Repositorio de conceptos manuales
    ''' </summary>
    ''' <remarks></remarks>
    Private _manualconceptsRepository As IManualConcepts

    Private _consecutiveRepository As IConsecutiveRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="manualconceptsRepository"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal manualconceptsRepository As IManualConcepts, consecutiveRepository As IConsecutiveRepository)
        If manualconceptsRepository Is Nothing Then
            Throw New ArgumentNullException("manualconceptsRepository Vacio")
        End If
        If consecutiveRepository Is Nothing Then
            Throw New ArgumentNullException("consecutiveRepository Vacio")
        End If
        _manualconceptsRepository = manualconceptsRepository
        _consecutiveRepository = consecutiveRepository
    End Sub

    ''' <summary>
    ''' Graba o Actualiza un concepto manual
    ''' </summary>
    ''' <param name="manualConcept">Concepto Manual</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveManualConcept(ByVal manualConcept As ManualConcepts, ByVal audit As AuditMessage) As ActionMessageResult Implements IManualConceptsAdminService.SaveManualConcept
        If manualConcept Is Nothing Then
            Throw New ArgumentNullException("manualConcept Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _manualconceptsRepository.UnitWork
        Dim unitOfWorkConsecutive As IUnitWork = _consecutiveRepository.UnitWork
        Try
            Dim consecutive As Domain.Entities.Consecutive = _consecutiveRepository.GetConsecutiveByCode("8")
            If manualConcept.ChangeTracker.State = ObjectState.Added Then
                'si es nuevo incrementamos el consecutivo
                manualConcept.Consecutive = CInt(consecutive.NumberConsecutive) + 1

                'Actualizamos el numero de consecutivo
                consecutive.NumberConsecutive += 1
                _consecutiveRepository.SaveEntity(consecutive)
                unitOfWorkConsecutive.Commit()
            End If

            If manualConcept.ContractNumber = 0 Then
                manualConcept.ContractNumber = manualConcept.ContractId
            End If


            _manualconceptsRepository.SaveEntity(manualConcept)
            UnitOfWork.Commit()

            If (manualConcept.ChangeTracker.State = ObjectState.Added) Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(manualConcept.GetType.Name, audit.Functional, manualConcept.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/*****Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of ManualConcepts)(manualConcept, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf (manualConcept.ChangeTracker.State = ObjectState.Modified) Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(manualConcept.GetType.Name, audit.Functional, manualConcept.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/*****Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of ManualConcepts)(manualConcept, audit, Infrastructure.CrossCutting.Audit.Actions.Update, manualConcept.ManualConceptsAux)
                auditObject.Execute()
            End If
            Return New ActionMessageResult With {.StateResult = True}
        Catch ex As Exception
            unitOfWorkConsecutive.RollbackChanges()
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionMessageResult() With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Funcion para obtener un concepto manual
    ''' </summary>
    ''' <param name="Consecutive">Numero de consecutivo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetManualConcepts(Consecutive As Integer) As ManualConcepts Implements IManualConceptsAdminService.GetManualConcepts
        If String.IsNullOrEmpty(Consecutive) Then
            Throw New ArgumentNullException("Consecutive Vacio")
        End If
        Try
            Return _manualconceptsRepository.GetManualConcepts(Consecutive)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ManualConcepts()
        End Try
    End Function

    ''' <summary>
    ''' Funcion que sirve para verificar un concepto manual ya ha sido registrado a un empleado
    ''' </summary>
    ''' <param name="_conceptId">id del concepto</param>
    ''' <param name="_contractNumber">numero de contrato</param>
    ''' <param name="_listDates">fecha que se quiere iniciar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetManualConceptsByConceptAndDate(_conceptId As Integer, _contractNumber As Integer, _listDates As List(Of Date), ProcessType As Byte, Optional _otherDate As Date = Nothing) As ManualConcepts Implements IManualConceptsAdminService.GetManualConceptsByConceptAndDate
        Try
            If _otherDate = Nothing Then
                Return _manualconceptsRepository.GetManualConceptsByConceptAndDate(_conceptId, _contractNumber, _listDates, ProcessType)
            Else
                Return _manualconceptsRepository.GetManualConceptsByConceptAndDate(_conceptId, _contractNumber, _listDates, ProcessType, _otherDate)
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Devuelve si ya hay un concepto manual ya registrado hasta fin de contrato, por concepto y por numero de contraro
    ''' </summary>
    ''' <param name="_conceptId">id del concepto</param>
    ''' <param name="_contractNumber">numero del contrato</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetManualConceptsByConceptAndEndContractTrue(_conceptId As Integer, _contractNumber As Integer, ProcessType As Byte) As Boolean Implements IManualConceptsAdminService.GetManualConceptsByConceptAndEndContractTrue
        Try
            Return _manualconceptsRepository.GetManualConceptsByConceptAndEndContractTrue(_conceptId, _contractNumber, ProcessType)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _manualconceptsRepository = Nothing
            _consecutiveRepository = Nothing
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

    Public Function ValidateManualConceptsMassive(pData As List(Of ImportFileRow)) As List(Of SP_ValidateMassiveManualConcepts_Result) Implements IManualConceptsAdminService.ValidateManualConceptsMassive
        Dim xmlObj As String = ConvertImportFileRowToXML(pData)
        Return _manualconceptsRepository.ValidateManualConceptsMassive(xmlObj)
    End Function

    Private Function ConvertImportFileRowToXML(pData As List(Of ImportFileRow)) As String
        Using sw As StringWriter = New StringWriter()
            Using xw As XmlWriter = XmlWriter.Create(sw)
                xw.WriteStartElement("Data")
                For Each ifr As ImportFileRow In pData
                    xw.WriteStartElement("Row")
                    xw.WriteElementString("Process", ifr.Row(0))
                    xw.WriteElementString("Nit", ifr.Row(1))
                    xw.WriteElementString("InternalCode", ifr.Row(2))
                    xw.WriteElementString("Code", ifr.Row(3))
                    xw.WriteElementString("QuoteValue", Convert.ToDouble(ifr.Row(4)).ToString("F2").Replace(",", "."))
                    xw.WriteElementString("PaidEndContract", ifr.Row(5))
                    xw.WriteElementString("QuoteNumber", ifr.Row(6))
                    xw.WriteElementString("PaidFormat", ifr.Row(7))
                    xw.WriteElementString("Description", ifr.Row(8))
                    xw.WriteEndElement()
                Next
                xw.WriteEndElement()
            End Using
            ConvertImportFileRowToXML = sw.ToString()
        End Using
    End Function

    Public Function GetManualConceptsMassive(pData As List(Of ImportFileRow)) As List(Of SP_GetMassiveManualConcepts_Result) Implements IManualConceptsAdminService.GetManualConceptsMassive
        Dim xmlObj As String = ConvertImportFileRowToXML(pData)
        Return _manualconceptsRepository.GetManualConceptsMassive(xmlObj)
    End Function

    Public Sub SaveManualConceptsMassive(pData As List(Of ImportFileRow)) Implements IManualConceptsAdminService.SaveManualConceptsMassive
        Dim xmlObj As String = ConvertImportFileRowToXML(pData)
        _manualconceptsRepository.SaveManualConceptsMassive(xmlObj)
    End Sub
End Class
