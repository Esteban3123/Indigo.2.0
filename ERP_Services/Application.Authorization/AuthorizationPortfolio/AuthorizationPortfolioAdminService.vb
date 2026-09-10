'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.SqlClient
Imports System.Text
Imports System.Transactions
Imports Application.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

Public Class AuthorizationPortfolioAdminService
    Implements IAuthorizationPortfolioAdminService

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As ISequenceAuthorizationDRepository
    Private _authorizationPortfolioRepository As IAuthorizationPortfolioRepository

    Public Sub New(secuenceDRepository As ISequenceAuthorizationDRepository, authorizationPortfolioRepository As IAuthorizationPortfolioRepository)
        If secuenceDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenceDRepository")
        End If
        If authorizationPortfolioRepository Is Nothing Then
            Throw New ArgumentNullException("authorizationGroupRepository")
        End If
        _secuenseDRepository = secuenceDRepository
        _authorizationPortfolioRepository = authorizationPortfolioRepository
    End Sub

    Public Function ChangeStateAuthorizationPortfolio(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of AuthorizationPortfolio) Implements IAuthorizationPortfolioAdminService.ChangeStateAuthorizationPortfolio
        Dim AuthorizationPortfolio As AuthorizationPortfolio = _authorizationPortfolioRepository.GetAuthorizationPortfolioByCode(code)
        AuthorizationPortfolio.Status = state
        Return SaveAuthorizationPortfolio(AuthorizationPortfolio, audit)
    End Function

    Public Function DeleteAuthorizationPortfolio(AuthorizationPortfolio As AuthorizationPortfolio, audit As AuditMessage) As ActionResult Implements IAuthorizationPortfolioAdminService.DeleteAuthorizationPortfolio
        If AuthorizationPortfolio Is Nothing Then
            Throw New ArgumentNullException("AuthorizationPortfolio")
        End If
        Using cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, AuthorizationPortfolio.TransactionalContainer, False))
            cnx.Open()
            Dim tx As System.Data.SqlClient.SqlTransaction = cnx.BeginTransaction()
            Dim command As New System.Data.SqlClient.SqlCommand("", cnx, tx)
                command.CommandTimeout = 30000
                    command.CommandType = CommandType.Text

                    Try
                        command.CommandText = "delete from [Authorization].AuthorizationPortfolioCUPSEntity where AuthorizationPortfolioId = " + AuthorizationPortfolio.Id.ToString()
                        command.ExecuteNonQuery()

                        command.CommandText = "delete from [Authorization].AuthorizationPortfolioInventoryProduct where AuthorizationPortfolioId = " + AuthorizationPortfolio.Id.ToString()
                        command.ExecuteNonQuery()

                        command.CommandText = "delete from [Authorization].AuthorizationPortfolioCareCenter where AuthorizationPortfolioId = " + AuthorizationPortfolio.Id.ToString()
                        command.ExecuteNonQuery()

                        command.CommandText = "delete from [Authorization].AuthorizationPortfolio where Id = " + AuthorizationPortfolio.Id.ToString()
                        command.ExecuteNonQuery()

                        tx.Commit()
                        Return New ActionResult With {.StateResult = True}
                    Catch ex As OptimisticConcurrencyException
                        tx.Rollback()
                        Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
                    Catch ex As UpdateException
                        tx.Rollback()
                        Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
                    Catch ex As Exception
                        tx.Rollback()
                        IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                        Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
                    Finally
                        cnx.Close()
                    End Try
                End Using
    End Function

    Public Function GetAuthorizationPortfolio(code As String, audit As AuditMessage) As ActionResult(Of AuthorizationPortfolio) Implements IAuthorizationPortfolioAdminService.GetAuthorizationPortfolio
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim AuthorizationPortfolio As AuthorizationPortfolio = Me._authorizationPortfolioRepository.GetAuthorizationPortfolioByCode(code.Trim())
            If AuthorizationPortfolio IsNot Nothing AndAlso AuthorizationPortfolio.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of AuthorizationPortfolio)(AuthorizationPortfolio, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If

            Return New ActionResult(Of AuthorizationPortfolio) With {.StateResult = True, .ObjectEmbbeded = AuthorizationPortfolio}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AuthorizationPortfolio) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetAuthorizationPortfolioById(id As Integer) As AuthorizationPortfolio Implements IAuthorizationPortfolioAdminService.GetAuthorizationPortfolioById
        Try
            Return _authorizationPortfolioRepository.GetAuthorizationPortfolioById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New AuthorizationPortfolio
        End Try
    End Function

    Public Function SaveAuthorizationPortfolio(AuthorizationPortfolio As AuthorizationPortfolio, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of AuthorizationPortfolio) Implements IAuthorizationPortfolioAdminService.SaveAuthorizationPortfolio
        If AuthorizationPortfolio Is Nothing Then
            Throw New ArgumentNullException("AuthorizationPortfolio")
        End If
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim xml = ConvertEntityToXml(AuthorizationPortfolio)

                Dim result = _authorizationPortfolioRepository.SP_SaveAuthorizationPortfolio(xml, audit.CodeUser)
                If result.CodeResult <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of AuthorizationPortfolio) With {.StateResult = False, .Message = result.MessageResult}
                End If

                AuthorizationPortfolio.Id = result.Id
                AuthorizationPortfolio.Code = result.Code

                scope.Complete()
                Return New ActionResult(Of AuthorizationPortfolio) With {.StateResult = True, .ObjectEmbbeded = AuthorizationPortfolio, .Message = result.MessageResult}
            Catch ex As Exception
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of AuthorizationPortfolio) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Private Function ConvertEntityToXml(authorizationPortfolio As AuthorizationPortfolio) As String
        Dim builder As New StringBuilder

        builder.Append("<AuthorizationPortfolio>")

        With authorizationPortfolio
            builder.Append("<Id>" & .Id & "</Id>")
            builder.Append("<OperatingUnitId>" & .OperatingUnitId & "</OperatingUnitId>")
            builder.Append("<Code>" & .Code & "</Code>")
            builder.Append("<Name>" & .Name & "</Name>")
            builder.Append("<Status>" & .Status & "</Status>")

            If .AuthorizationPortfolioCUPSEntity IsNot Nothing AndAlso .AuthorizationPortfolioCUPSEntity.Count > 0 Then
                For Each item In .AuthorizationPortfolioCUPSEntity
                    builder.Append("<AuthorizationPortfolioCUPSEntity>")
                    builder.Append("<Id>" & item.Id & "</Id>")
                    builder.Append("<AuthorizationPortfolioId>" & item.AuthorizationPortfolioId & "</AuthorizationPortfolioId>")
                    builder.Append("<AuthorizationGroupId>" & item.AuthorizationGroupId & "</AuthorizationGroupId>")
                    builder.Append("<CUPSEntityId>" & item.CUPSEntityId & "</CUPSEntityId>")
                    If item.ContractDescriptionId IsNot Nothing Then
                        builder.Append("<ContractDescriptionId>" & item.ContractDescriptionId & "</ContractDescriptionId>")
                    End If
                    builder.Append("<IsDelete>" & If(item.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")
                    builder.Append("</AuthorizationPortfolioCUPSEntity>")
                Next
            End If

            If .AuthorizationPortfolioInventoryProduct IsNot Nothing AndAlso .AuthorizationPortfolioInventoryProduct.Count > 0 Then
                For Each item In .AuthorizationPortfolioInventoryProduct
                    builder.Append("<AuthorizationPortfolioInventoryProduct>")
                    builder.Append("<Id>" & item.Id & "</Id>")
                    builder.Append("<AuthorizationPortfolioId>" & item.AuthorizationPortfolioId & "</AuthorizationPortfolioId>")
                    builder.Append("<AuthorizationGroupId>" & item.AuthorizationGroupId & "</AuthorizationGroupId>")
                    builder.Append("<InventoryProductId>" & item.InventoryProductId & "</InventoryProductId>")
                    builder.Append("<IsDelete>" & If(item.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")
                    builder.Append("</AuthorizationPortfolioInventoryProduct>")
                Next
            End If

            If .AuthorizationPortfolioCareCenter IsNot Nothing AndAlso .AuthorizationPortfolioCareCenter.Count > 0 Then
                For Each item In .AuthorizationPortfolioCareCenter
                    builder.Append("<AuthorizationPortfolioCareCenter>")
                    builder.Append("<Id>" & item.Id & "</Id>")
                    builder.Append("<AuthorizationPortfolioId>" & item.AuthorizationPortfolioId & "</AuthorizationPortfolioId>")
                    builder.Append("<CareCenterCode>" & item.CareCenterCode & "</CareCenterCode>")
                    builder.Append("<IsDelete>" & If(item.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")
                    builder.Append("</AuthorizationPortfolioCareCenter>")
                Next
            End If
        End With

        builder.Append("</AuthorizationPortfolio>")

        Return builder.ToString()
    End Function

    Public Function SP_CopyAndPasteAuthorizationPortfolioCareCenter(data As List(Of List(Of String))) As ActionResult(Of List(Of AuthorizationPortfolioCareCenter), List(Of Tuple(Of String, Integer))) Implements IAuthorizationPortfolioAdminService.SP_CopyAndPasteAuthorizationPortfolioCareCenter
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If
        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListAuthorizationPortfolioCareCenter As New List(Of AuthorizationPortfolioCareCenter)
        'Listado de errores
        Dim listErrors As New List(Of Tuple(Of String, Integer))
        Try
            'Objeto xml
            Dim xmlObject = ConvertToXmlCareCenter(data)
            'Se consume el procedimiento almacenado
            Dim resultStore = _authorizationPortfolioRepository.SP_CopyAndPasteAuthorizationPortfolioCareCenter(xmlObject)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 1 Then 'Si el estado del item es True y pasó todas las validaciones
                        If ListAuthorizationPortfolioCareCenter IsNot Nothing AndAlso ListAuthorizationPortfolioCareCenter.Count > 0 Then
                            Dim itemAddeed = ListAuthorizationPortfolioCareCenter.Find(Function(x) x.CareCenterCode = itemXml.CareCenterCode)
                            If itemAddeed IsNot Nothing Then
                                Continue For
                            End If
                        End If
                        Dim AuthorizationPortfolioCareCenter As New AuthorizationPortfolioCareCenter
                        With AuthorizationPortfolioCareCenter
                            .CareCenterCode = itemXml.CareCenterCode
                            .CareCenterDescription = itemXml.CareCenterCodeName
                        End With
                        ListAuthorizationPortfolioCareCenter.Add(AuthorizationPortfolioCareCenter)
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(New Tuple(Of String, Integer)(itemXml.MessageField, 2))
                    End If
                Next
            End If


            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of AuthorizationPortfolioCareCenter), List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = ListAuthorizationPortfolioCareCenter, .ObjectEmbbededAux = listErrors, .StateResult = True}
        Catch ex As SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of AuthorizationPortfolioCareCenter), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of AuthorizationPortfolioCareCenter), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of AuthorizationPortfolioCareCenter), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

    Private Function ConvertToXmlCareCenter(data As List(Of List(Of String))) As String
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        For Each item In data
            builder.Append("<Row>")

            builder.Append("<CountFields>" & item.Count & "</CountFields>")
            builder.Append("<StatusField>" & 0 & "</StatusField>")
            builder.Append("<MessageField>" & "Ok" & "</MessageField>")
            builder.Append("<CareCenterCode>" & item(0) & "</CareCenterCode>")
            builder.Append("<CareCenterCodeName>" & "---" & "</CareCenterCodeName>")

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    Public Function SP_CopyAndPasteAuthorizationPortfolioCUPSEntity(data As List(Of List(Of String))) As ActionResult(Of List(Of AuthorizationPortfolioCUPSEntity), List(Of Tuple(Of String, Integer))) Implements IAuthorizationPortfolioAdminService.SP_CopyAndPasteAuthorizationPortfolioCUPSEntity
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If
        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListAuthorizationPortfolioCUPSEntity As New List(Of AuthorizationPortfolioCUPSEntity)
        'Listado de errores
        Dim listErrors As New List(Of Tuple(Of String, Integer))
        Try
            'Objeto xml
            Dim xmlObject = ConvertToXmlCUPS(data)
            'Se consume el procedimiento almacenado
            Dim resultStore = _authorizationPortfolioRepository.SP_CopyAndPasteAuthorizationPortfolioCUPSEntity(xmlObject)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 1 Then 'Si el estado del item es True y pasó todas las validaciones
                        If ListAuthorizationPortfolioCUPSEntity IsNot Nothing AndAlso ListAuthorizationPortfolioCUPSEntity.Count > 0 Then
                            Dim itemAddeed = ListAuthorizationPortfolioCUPSEntity.Find(Function(x) x.CUPSEntityId = itemXml.CUPSEntityId AndAlso (If(x.ContractDescriptionId Is Nothing, 0, x.ContractDescriptionId) = If(itemXml.ContractDescriptionId Is Nothing, 0, itemXml.ContractDescriptionId)))
                            If itemAddeed IsNot Nothing Then
                                Continue For
                            End If
                        End If
                        Dim AuthorizationPortfolioCUPSEntity As New AuthorizationPortfolioCUPSEntity
                        With AuthorizationPortfolioCUPSEntity
                            .CUPSEntityId = itemXml.CUPSEntityId
                            .CUPSEntityDescription = itemXml.CUPSEntityCodeName
                            .CUPSEntityCode = itemXml.CUPSEntityCode
                            .CUPSEntityName = itemXml.CUPSEntityName
                            .AuthorizationGroupId = itemXml.AuthorizationGroupId
                            .AuthorizationGroupDescription = itemXml.AuthorizationGroupCodeName

                            If itemXml.ContractDescriptionId IsNot Nothing AndAlso itemXml.ContractDescriptionId > 0 Then
                                .ContractDescriptionId = itemXml.ContractDescriptionId
                                .ContractDescriptionDescription = itemXml.ContractDescriptionCodeName
                            End If
                        End With
                        ListAuthorizationPortfolioCUPSEntity.Add(AuthorizationPortfolioCUPSEntity)
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(New Tuple(Of String, Integer)(itemXml.MessageField, 2))
                    End If
                Next
            End If


            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of AuthorizationPortfolioCUPSEntity), List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = ListAuthorizationPortfolioCUPSEntity, .ObjectEmbbededAux = listErrors, .StateResult = True}
        Catch ex As SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of AuthorizationPortfolioCUPSEntity), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of AuthorizationPortfolioCUPSEntity), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of AuthorizationPortfolioCUPSEntity), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

    Private Function ConvertToXmlCUPS(data As List(Of List(Of String))) As String
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        For Each item In data
            builder.Append("<Row>")

            builder.Append("<CountFields>" & item.Count & "</CountFields>")
            builder.Append("<StatusField>" & 0 & "</StatusField>")
            builder.Append("<MessageField>" & "Ok" & "</MessageField>")
            builder.Append("<AuthorizationGroupId>" & 0 & "</AuthorizationGroupId>")
            builder.Append("<AuthorizationGroupCode>" & item(0) & "</AuthorizationGroupCode>")
            builder.Append("<AuthorizationGroupCodeName>" & "---" & "</AuthorizationGroupCodeName>")

            builder.Append("<CUPSEntityId>" & 0 & "</CUPSEntityId>")
            If item.Count > 1 Then
                builder.Append("<CUPSEntityCode>" & item(1) & "</CUPSEntityCode>")
            Else
                builder.Append("<CUPSEntityCode>" & "" & "</CUPSEntityCode>")
            End If
            builder.Append("<CUPSEntityName>" & "---" & "</CUPSEntityName>")
            builder.Append("<CUPSEntityCodeName>" & "---" & "</CUPSEntityCodeName>")

            builder.Append("<ContractDescriptionId>" & 0 & "</ContractDescriptionId>")
            If item.Count > 2 Then
                builder.Append("<ContractDescriptionCode>" & item(2) & "</ContractDescriptionCode>")
            Else
                builder.Append("<ContractDescriptionCode>" & "" & "</ContractDescriptionCode>")
            End If
            builder.Append("<ContractDescriptionCodeName>" & "---" & "</ContractDescriptionCodeName>")

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    Public Function SP_CopyAndPasteAuthorizationPortfolioInventoryProduct(data As List(Of List(Of String))) As ActionResult(Of List(Of AuthorizationPortfolioInventoryProduct), List(Of Tuple(Of String, Integer))) Implements IAuthorizationPortfolioAdminService.SP_CopyAndPasteAuthorizationPortfolioInventoryProduct
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If
        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListAuthorizationPortfolioInventoryProduct As New List(Of AuthorizationPortfolioInventoryProduct)
        'Listado de errores
        Dim listErrors As New List(Of Tuple(Of String, Integer))
        Try
            'Objeto xml
            Dim xmlObject = ConvertToXmlProduct(data)
            'Se consume el procedimiento almacenado
            Dim resultStore = _authorizationPortfolioRepository.SP_CopyAndPasteAuthorizationPortfolioInventoryProduct(xmlObject)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 1 Then 'Si el estado del item es True y pasó todas las validaciones
                        If ListAuthorizationPortfolioInventoryProduct IsNot Nothing AndAlso ListAuthorizationPortfolioInventoryProduct.Count > 0 Then
                            Dim itemAddeed = ListAuthorizationPortfolioInventoryProduct.Find(Function(x) x.InventoryProductId = itemXml.InventoryProductId)
                            If itemAddeed IsNot Nothing Then
                                Continue For
                            End If
                        End If
                        Dim AuthorizationPortfolioInventoryProduct As New AuthorizationPortfolioInventoryProduct
                        With AuthorizationPortfolioInventoryProduct
                            .InventoryProductId = itemXml.InventoryProductId
                            .InventoryProductDescription = itemXml.InventoryProductCodeName
                            .ProductCode = itemXml.InventoryProductCode
                            .ProductName = itemXml.InventoryProductName
                            .AuthorizationGroupId = itemXml.AuthorizationGroupId
                            .AuthorizationGroupDescription = itemXml.AuthorizationGroupCodeName
                        End With
                        ListAuthorizationPortfolioInventoryProduct.Add(AuthorizationPortfolioInventoryProduct)
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(New Tuple(Of String, Integer)(itemXml.MessageField, 2))
                    End If
                Next
            End If


            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of AuthorizationPortfolioInventoryProduct), List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = ListAuthorizationPortfolioInventoryProduct, .ObjectEmbbededAux = listErrors, .StateResult = True}
        Catch ex As SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of AuthorizationPortfolioInventoryProduct), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of AuthorizationPortfolioInventoryProduct), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of AuthorizationPortfolioInventoryProduct), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

    Private Function ConvertToXmlProduct(data As List(Of List(Of String))) As String
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        For Each item In data
            builder.Append("<Row>")

            builder.Append("<CountFields>" & item.Count & "</CountFields>")
            builder.Append("<StatusField>" & 0 & "</StatusField>")
            builder.Append("<MessageField>" & "Ok" & "</MessageField>")
            builder.Append("<AuthorizationGroupId>" & 0 & "</AuthorizationGroupId>")
            builder.Append("<AuthorizationGroupCode>" & item(0) & "</AuthorizationGroupCode>")
            builder.Append("<AuthorizationGroupCodeName>" & "---" & "</AuthorizationGroupCodeName>")

            builder.Append("<InventoryProductId>" & 0 & "</InventoryProductId>")
            If item.Count > 1 Then
                builder.Append("<InventoryProductCode>" & item(1) & "</InventoryProductCode>")
            Else
                builder.Append("<InventoryProductCode>" & "" & "</InventoryProductCode>")
            End If
            builder.Append("<InventoryProductName>" & "---" & "</InventoryProductName>")
            builder.Append("<InventoryProductCodeName>" & "---" & "</InventoryProductCodeName>")

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
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _secuenseDRepository = Nothing
            _authorizationPortfolioRepository = Nothing
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
