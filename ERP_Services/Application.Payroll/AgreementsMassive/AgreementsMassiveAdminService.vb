'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 08/09/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports Application.Payroll
Imports Domain.Entities
Imports System.Text
Imports System.Data.SqlClient
Imports System.Data.Entity.Core
Imports System.Transactions

Public Class AgreementsMassiveAdminService
    Implements IAgreementsMassiveAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _AgreementsMassiveRepository As IAgreementsMassiveRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(IAgreementsMassiveRepository As IAgreementsMassiveRepository)
        If IAgreementsMassiveRepository Is Nothing Then
            Throw New ArgumentNullException("IAgreementsMassiveRepository Vacio")
        End If
        _AgreementsMassiveRepository = IAgreementsMassiveRepository
    End Sub

#End Region

    Public Function SP_ImportFileAgreementsMassive(data As List(Of ImportFileRow)) As ActionResult(Of List(Of SP_ImportFileAgreementsC_Result)) Implements IAgreementsMassiveAdminService.SP_ImportFileAgreementsMassive
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If
        Try
            'Objeto xml
            Dim xmlObject = ConvertToXmlByImportFile(data)

            'Se consume el procedimiento almacenado
            Dim resultStore = _AgreementsMassiveRepository.SP_ImportFileAgreementsMassive(xmlObject)

            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of SP_ImportFileAgreementsC_Result)) With {.StateResult = True, .ObjectEmbbeded = resultStore}
        Catch ex As SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of SP_ImportFileAgreementsC_Result)) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of SP_ImportFileAgreementsC_Result)) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of SP_ImportFileAgreementsC_Result)) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function



    ''' <summary>
    ''' Metodo que me convierte el listado de datos en xml cuando es por importacion
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Private Function ConvertToXmlByImportFile(data As List(Of ImportFileRow)) As String
        'String de xml que se arma con el listado
        Dim builder As StringBuilder = New StringBuilder()

        'Cantidad de item para validar 
        Dim count As Integer = 0

        builder.Append("<Data>")

        For Each info In data

            'Se asigna la cantidad de items
            count = info.Row.Count

            builder.Append("<Row>")

            builder.Append("<CountFields>" & info.Row.Count & "</CountFields>")
            builder.Append("<StatusField>" & 0 & "</StatusField>")
            builder.Append("<MessageField>" & "-" & "</MessageField>")

            If count > 0 Then
                builder.Append("<Nit>" & info.Row(0) & "</Nit>")
                count -= 1
            Else
                builder.Append("<Nit></Nit>")
            End If

            builder.Append("<NitName>" & "-" & "</NitName>")
            builder.Append("<EmployeeId>" & 0 & "</EmployeeId>")

            If count > 0 Then
                builder.Append("<InternalCode>" & info.Row(1) & "</InternalCode>")
            Else
                builder.Append("<InternalCode></InternalCode>")
            End If

            If count > 0 Then
                builder.Append("<ConceptCode>" & info.Row(2) & "</ConceptCode>")
                count -= 1
            Else
                builder.Append("<ConceptCode></ConceptCode>")
            End If

            builder.Append("<ConceptId>" & 0 & "</ConceptId>")

            If count > 0 Then
                builder.Append("<PayrollDate>" & info.Row(3) & "</PayrollDate>")
                count -= 1
            Else
                builder.Append("<PayrollDate></PayrollDate>")
            End If

            If count > 0 Then
                builder.Append("<NoveltyType>" & info.Row(4) & "</NoveltyType>")
                count -= 1
            Else
                builder.Append("<NoveltyType></NoveltyType>")
            End If

            If count > 0 Then
                builder.Append("<QuoteValue>" & info.Row(5) & "</QuoteValue>")
                count -= 1
            Else
                builder.Append("<QuoteValue></QuoteValue>")
            End If

            If count > 0 Then
                builder.Append("<NoveltyBalance>" & info.Row(6) & "</NoveltyBalance>")
                count -= 1
            Else
                builder.Append("<NoveltyBalance></NoveltyBalance>")
            End If

            If count > 0 Then
                builder.Append("<NitCompany>" & info.Row(7) & "</NitCompany>")
                count -= 1
            Else
                builder.Append("<NitCompany></NitCompany>")
            End If

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    Public Function SP_SaveAgreementsMassive(ListInfo As List(Of SP_ImportFileAgreementsC_Result), Audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer))) Implements IAgreementsMassiveAdminService.SP_SaveAgreementsMassive
        If ListInfo Is Nothing OrElse ListInfo.Count = 0 Then
            Throw New ArgumentNullException("ListInfo")
        End If

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                'Objeto xml
                Dim xmlObject = ConvertToXmlBySave(ListInfo)

                'Se envia la info al sp
                Dim resultStore = _AgreementsMassiveRepository.SP_SaveAgreementsMassive(xmlObject, Audit.CodeUser)

                If resultStore Is Nothing OrElse resultStore.Count = 0 Then 'Si no hay datos
                    Transaction.Dispose()
                    Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = "No se pudo guardar las liquidaciones"}
                End If

                If (From x In resultStore Where x.CodeResult = 888 Select x).Count > 0 Then 'Se valida si hay algun error de catch en el sql
                    Transaction.Dispose()
                    Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = (From x In resultStore Where x.CodeResult = 888 Select x.MessageResult).FirstOrDefault()}
                End If

                'Listado de errores y de oks
                Dim ListReturn As New List(Of Tuple(Of String, Integer))

                'Se recorre la info para armar el listado de ok o error
                resultStore.ForEach(Sub(x) ListReturn.Add(New Tuple(Of String, Integer)(x.MessageResult, IIf(x.CodeResult = 0, 1, 2))))

                Transaction.Complete()
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = True, .ObjectEmbbeded = ListReturn}
            Catch ex As OptimisticConcurrencyException
                Transaction.Dispose()
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.Message}
            Catch ex As Exception
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.Message}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Metodo que me convierte el listado de datos en xml cuando es para guardar
    ''' </summary>
    ''' <returns></returns>
    Private Function ConvertToXmlBySave(ListInfo As List(Of SP_ImportFileAgreementsC_Result)) As String
        'String de xml que se arma con el listado
        Dim builder As StringBuilder = New StringBuilder()

        'Cantidad de item para validar 
        Dim count As Integer = 0

        builder.Append("<Data>")

        For Each info In ListInfo

            builder.Append("<Row>")

            builder.Append("<Nit>" & info.Nit & "</Nit>")
            builder.Append("<InternalCode>" & info.InternalCode & "</InternalCode>")
            builder.Append("<ConceptCode>" & info.ConceptCode & "</ConceptCode>")
            builder.Append("<PaidDate>" & info.PayrollDate & "</PaidDate>")
            builder.Append("<AgreementsTypeCode>" & 0 & "</AgreementsTypeCode>")
            builder.Append("<NoveltyType>" & info.NoveltyType & "</NoveltyType>")
            builder.Append("<QuoteValue>" & info.QuoteValue & "</QuoteValue>")
            builder.Append("<AgreementsBalance>" & info.NoveltyBalance & "</AgreementsBalance>")
            builder.Append("<NitCompany>" & info.NitCompany & "</NitCompany>")

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _AgreementsMassiveRepository = Nothing
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
