#Region "Imports"

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Core
Imports System.Transactions
Imports System.Text

#End Region

Public Class ProductAndServiceFeeAdminService
    Implements IProductAndServiceFeeAdminService

#Region "Properties"

    Private _ProductAndServiceFeeRepository As IProductAndServiceFeeRepository

#End Region

#Region "Builder"

    Public Sub New(ProductAndServiceFeeRepository As IProductAndServiceFeeRepository)

        If ProductAndServiceFeeRepository Is Nothing Then
            Throw New ArgumentNullException("ProductAndServiceFeeRepository")
        End If

        Me._ProductAndServiceFeeRepository = ProductAndServiceFeeRepository
    End Sub

#End Region

#Region "Methods"

    Public Function GetProductAndServiceFeeById(Id As Integer, audit As AuditMessage) As ProductAndServiceFee Implements IProductAndServiceFeeAdminService.GetProductAndServiceFeeById
        If String.IsNullOrEmpty(Id) Then
            Throw New ArgumentNullException("Id")
        End If

        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Try
            Dim ProductAndServiceFee = Me._ProductAndServiceFeeRepository.GetProductAndServiceFeeById(Id)
            Return ProductAndServiceFee
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ProductAndServiceFee()
        End Try
    End Function

    Public Function GetProductAndServiceFeeByCode(code As String, audit As AuditMessage) As ProductAndServiceFee Implements IProductAndServiceFeeAdminService.GetProductAndServiceFeeByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If

        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Try
            Dim ProductAndServiceFee = Me._ProductAndServiceFeeRepository.GetProductAndServiceFeeByCode(code)
            Return ProductAndServiceFee
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ProductAndServiceFee()
        End Try
    End Function

    Public Function UpdateStateProductAndServiceFee(Code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ProductAndServiceFee) Implements IProductAndServiceFeeAdminService.UpdateStateProductAndServiceFee
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Code")
        End If

        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If

        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Try
            Dim ProductAndServiceFee = Me._ProductAndServiceFeeRepository.GetProductAndServiceFeeByCode(Code)
            If ProductAndServiceFee IsNot Nothing AndAlso ProductAndServiceFee.Id > 0 Then
                ProductAndServiceFee.Status = state
            End If

            Return SaveProductAndServiceFee(ProductAndServiceFee, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ProductAndServiceFee) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
        End Try
    End Function

    Public Function SetProductFeeDetailDetailFromFile(dataimport As List(Of ImportFileRow), data As List(Of List(Of String))) As ActionResult(Of List(Of ProductFeeDetail)) Implements IProductAndServiceFeeAdminService.SetProductFeeDetailFromFile
        Try

            'Objeto xml
            Dim xmlObject As String = String.Empty

            'Listado de datos copiados o importados que se devuelven a la rejilla de productos
            Dim ListProductFeeDetail As New List(Of ProductFeeDetail)

            'Listado de registros con errores
            Dim listRecordsErrors As New List(Of String())

            'Listado de errores
            Dim listErrors As New List(Of String)

            If dataimport IsNot Nothing Then
                xmlObject = ConvertToXmlImportFile(dataimport, True)
            Else
                xmlObject = ConvertToXmlCopyPaste(data, True)
            End If

            'Se consume el procedimiento almacenado
            Dim resultStore = Me._ProductAndServiceFeeRepository.SetProductFeeDetailFromFile(xmlObject)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 0 Then
                        'Se crea el nuevo objeto para agregarlo al listado
                        ListProductFeeDetail.Add(New ProductFeeDetail With
                        {
                            .ProductCodeName = itemXml.Product,
                            .ProductId = itemXml.ProductId,
                            .RateType = itemXml.RateTypeOut,
                            .RateTypeName = If(.RateType, "Tarifa Fija", "Porcentaje"),
                            .Observations = itemXml.Observations,
                            .InitialDate = itemXml.InitialDateOut,
                            .FinalDate = itemXml.FinalDateOut,
                            .SalePrice = itemXml.SalePriceOut,
                            .PercentageType = IIf(Not .RateType, itemXml.PercentageTypeOut, Nothing),
                            .PercentageTypeName = IIf(Not .RateType, If(Not .PercentageType, "Costo Promedio Ponderado", "Ultimo Costo"), Nothing),
                            .Percentage = IIf(Not .RateType, itemXml.PercentageOut, Nothing)
                        })
                    Else 'Si el estado del item es False y no pasó alguna validación

                        If itemXml.Product IsNot Nothing Then
                            If itemXml.Product.Contains("-") Then
                                Dim _Product = itemXml.Product.Split("-")
                                itemXml.Product = _Product(0)
                            End If
                        End If

                        Dim datos As String() = {itemXml.Product, itemXml.RateType, itemXml.Observations, String.Concat(itemXml.InitialDateOut),
                                                String.Concat(itemXml.FinalDateOut), itemXml.SalePriceOut, itemXml.PercentageType, String.Concat(itemXml.PercentageOut), itemXml.MessageField}

                        listRecordsErrors.Add(datos)
                        listErrors.Add(itemXml.MessageField)
                    End If
                Next
            End If

            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of ProductFeeDetail)) With {.StateResult = True, .ObjectEmbbeded = ListProductFeeDetail, .MessageResult = listErrors, .ListMessageResult = listRecordsErrors}

        Catch ex As SqlClient.SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of ProductFeeDetail)) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of ProductFeeDetail)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of ProductFeeDetail)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function SetServicesFeeDetailDetailFromFile(dataimport As List(Of ImportFileRow), data As List(Of List(Of String))) As ActionResult(Of List(Of ServiceFeeDetail)) Implements IProductAndServiceFeeAdminService.SetServicesFeeDetailFromFile
        Try

            'Objeto xml
            Dim xmlObject As String = String.Empty

            'Listado de datos copiados o importados que se devuelven a la rejilla de servicios
            Dim ListServiceFeeDetail As New List(Of ServiceFeeDetail)

            'Listado de registros con errores
            Dim listRecordsErrors As New List(Of String())

            'Listado de errores
            Dim listErrors As New List(Of String)

            If dataimport IsNot Nothing Then
                xmlObject = ConvertToXmlImportFile(dataimport, False)
            Else
                xmlObject = ConvertToXmlCopyPaste(data, False)
            End If

            'Se consume el procedimiento almacenado
            Dim resultStore = Me._ProductAndServiceFeeRepository.SetServiceProductFeeDetailFromFile(xmlObject)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 0 Then
                        'Se crea el nuevo objeto para agregarlo al listado
                        ListServiceFeeDetail.Add(New ServiceFeeDetail With
                        {
                            .ServiceCodeName = itemXml.Service,
                            .ServiceId = itemXml.ServiceId,
                            .Observations = itemXml.Observations,
                            .InitialDate = itemXml.InitialDateOut,
                            .FinalDate = itemXml.FinalDateOut,
                            .SalePrice = itemXml.SalePriceOut
                        })
                    Else 'Si el estado del item es False y no pasó alguna validación

                        If itemXml.Service IsNot Nothing Then
                            If itemXml.Service.Contains("-") Then
                                Dim _Service = itemXml.Service.Split("-")
                                itemXml.Service = _Service(0)
                            End If
                        End If

                        Dim datos As String() = {itemXml.Service, itemXml.Observations, String.Concat(itemXml.InitialDateOut),
                                                String.Concat(itemXml.FinalDateOut), itemXml.SalePriceOut, itemXml.MessageField}

                        listRecordsErrors.Add(datos)
                        listErrors.Add(itemXml.MessageField)
                    End If
                Next
            End If

            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of ServiceFeeDetail)) With {.StateResult = True, .ObjectEmbbeded = ListServiceFeeDetail, .MessageResult = listErrors, .ListMessageResult = listRecordsErrors}

        Catch ex As SqlClient.SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of ServiceFeeDetail)) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of ServiceFeeDetail)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of ServiceFeeDetail)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function SaveProductAndServiceFee(ProductAndServiceFee As ProductAndServiceFee, audit As AuditMessage) As ActionResult(Of ProductAndServiceFee) Implements IProductAndServiceFeeAdminService.SaveProductAndServiceFee
        If ProductAndServiceFee Is Nothing Then
            Throw New ArgumentNullException("ProductAndServiceFee")
        End If

        Dim unitOfWork As IUnitWork = Me._ProductAndServiceFeeRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try

                Dim EntityXml As String = ConvertToXmlProductAndServiceFee(ProductAndServiceFee)

                Dim resultStore = Me._ProductAndServiceFeeRepository.SP_SaveProductAndServiceFee(EntityXml, audit.CodeUser)
                If resultStore.CodeMessage <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of ProductAndServiceFee) With {.StateResult = False, .MessageResult = {resultStore.Message}.ToList, .Message = resultStore.Message}
                End If

                ProductAndServiceFee.Id = resultStore.Id
                ProductAndServiceFee.Code = resultStore.Code
                ProductAndServiceFee.MarkAsUnchanged()
                transaction.Complete()

                Return New ActionResult(Of ProductAndServiceFee) With {.StateResult = True, .ObjectEmbbeded = ProductAndServiceFee, .Message = resultStore.Message}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of ProductAndServiceFee) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of ProductAndServiceFee) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function
#End Region

#Region "Private Properties"

    Private Function ConvertToXmlCopyPaste(data As List(Of List(Of String)), flag As Boolean)
        Dim builder As StringBuilder = New StringBuilder()

        If flag Then
            builder.Append("<Data>")

            Dim indexRow As Integer = 0
            For Each item In data

                indexRow = indexRow + 1
                Dim Columns = item.Count

                builder.Append("<Row>")

                builder.Append("<RowIndex>" & indexRow & "</RowIndex>")
                builder.Append("<RowColumns>" & Columns & "</RowColumns>")
                builder.Append("<Product>" & If(Columns > 0, item(0), String.Empty) & "</Product>")
                builder.Append("<RateType>" & If(Columns > 1, item(1), String.Empty) & "</RateType>")
                builder.Append("<PercentageType>" & If(Columns > 2, item(2), String.Empty) & "</PercentageType>")
                builder.Append("<InitialDate>" & If(Columns > 3, item(3), String.Empty) & "</InitialDate>")
                builder.Append("<FinalDate>" & If(Columns > 4, item(4), String.Empty) & "</FinalDate>")
                builder.Append("<Percentage>" & If(Columns > 5, item(5).ToString.Replace(",", "."), String.Empty) & "</Percentage>")
                builder.Append("<SalePrice>" & If(Columns > 6, item(6).ToString.Replace(",", "."), String.Empty) & "</SalePrice>")
                builder.Append("<Observations>" & If(Columns > 7, item(7), String.Empty) & "</Observations>")

                builder.Append("</Row>")

            Next
            builder.Append("</Data>")

        Else
            builder.Append("<Data>")

            Dim indexRow As Integer = 0
            For Each item In data

                indexRow = indexRow + 1
                Dim Columns = item.Count

                builder.Append("<Row>")

                builder.Append("<RowIndex>" & indexRow & "</RowIndex>")
                builder.Append("<RowColumns>" & Columns & "</RowColumns>")
                builder.Append("<Service>" & If(Columns > 0, item(0), String.Empty) & "</Service>")
                builder.Append("<InitialDate>" & If(Columns > 1, item(1), String.Empty) & "</InitialDate>")
                builder.Append("<FinalDate>" & If(Columns > 2, item(2), String.Empty) & "</FinalDate>")
                builder.Append("<SalePrice>" & If(Columns > 3, item(3).ToString.Replace(",", "."), String.Empty) & "</SalePrice>")
                builder.Append("<Observations>" & If(Columns > 4, item(4), String.Empty) & "</Observations>")

                builder.Append("</Row>")

            Next
            builder.Append("</Data>")
        End If

        Return builder.ToString
    End Function

    Private Function ConvertToXmlImportFile(data As List(Of ImportFileRow), flag As Boolean)
        Dim builder As StringBuilder = New StringBuilder()

        If flag Then
            builder.Append("<Data>")

            For Each item In data

                Dim indexRow = item.IndexRow
                Dim Columns = item.Row.Count

                builder.Append("<Row>")

                builder.Append("<RowIndex>" & indexRow & "</RowIndex>")
                builder.Append("<RowColumns>" & Columns & "</RowColumns>")
                builder.Append("<Product>" & If(Columns > 0, item.Row.Item(0), String.Empty) & "</Product>")
                builder.Append("<RateType>" & If(Columns > 1, item.Row.Item(1), String.Empty) & "</RateType>")
                builder.Append("<PercentageType>" & If(Columns > 2, item.Row.Item(2), String.Empty) & "</PercentageType>")
                builder.Append("<InitialDate>" & If(Columns > 3, item.Row.Item(3), String.Empty) & "</InitialDate>")
                builder.Append("<FinalDate>" & If(Columns > 4, item.Row.Item(4), String.Empty) & "</FinalDate>")
                builder.Append("<Percentage>" & If(Columns > 5, item.Row.Item(5)?.ToString.Replace(",", "."), String.Empty) & "</Percentage>")
                builder.Append("<SalePrice>" & If(Columns > 6, item.Row.Item(6)?.ToString.Replace(",", "."), String.Empty) & "</SalePrice>")
                builder.Append("<Observations>" & If(Columns > 7, item.Row.Item(7), String.Empty) & "</Observations>")

                builder.Append("</Row>")

            Next
            builder.Append("</Data>")
        Else
            builder.Append("<Data>")

            For Each item In data

                Dim indexRow = item.IndexRow
                Dim Columns = item.Row.Count

                builder.Append("<Row>")

                builder.Append("<RowIndex>" & indexRow & "</RowIndex>")
                builder.Append("<RowColumns>" & Columns & "</RowColumns>")
                builder.Append("<Service>" & If(Columns > 0, item.Row.Item(0), String.Empty) & "</Service>")
                builder.Append("<InitialDate>" & If(Columns > 1, item.Row.Item(1), String.Empty) & "</InitialDate>")
                builder.Append("<FinalDate>" & If(Columns > 2, item.Row.Item(2), String.Empty) & "</FinalDate>")
                builder.Append("<SalePrice>" & If(Columns > 3, item.Row.Item(3)?.ToString.Replace(",", "."), String.Empty) & "</SalePrice>")
                builder.Append("<Observations>" & If(Columns > 4, item.Row.Item(4), String.Empty) & "</Observations>")

                builder.Append("</Row>")

            Next
            builder.Append("</Data>")
        End If

        Return builder.ToString
    End Function

    Private Function ConvertToXmlProductAndServiceFee(ProductAndServiceFee As ProductAndServiceFee) As String
        Dim Builder As StringBuilder = New StringBuilder()

        Dim rowProductFeeDetail = 1
        Dim rowServiceFeeDetail = 1
        Dim rowUserDetail = 1

        Builder.Append("<ProductAndServiceFee>")

        builder.Append("<Id>" & ProductAndServiceFee.Id & "</Id>")
        builder.Append("<Code>" & ProductAndServiceFee.Code & "</Code>")
        builder.Append("<Name>" & ProductAndServiceFee.Name & "</Name>")
        Builder.Append("<Status>" & IIf(ProductAndServiceFee.Status, 1, 0) & "</Status>")
        Builder.Append("<OperatingUnitId>" & ProductAndServiceFee.OperatingUnitId & "</OperatingUnitId>")

        For Each detail In ProductAndServiceFee.ProductFeeDetail
            builder.Append("<ProductFeeDetail>")

            builder.Append("<Id>" & detail.Id & "</Id>")
            builder.Append("<TempId>" & rowProductFeeDetail & "</TempId>")
            builder.Append("<ProductAndServiceFeeId>" & detail.ProductAndServiceFeeId & "</ProductAndServiceFeeId>")
            builder.Append("<ProductId>" & detail.ProductId & "</ProductId>")
            Builder.Append("<RateType>" & IIf(detail.RateType, 1, 0) & "</RateType>")
            Builder.Append("<PercentageType>" & IIf(detail.RateType, detail.PercentageType, Nothing) & "</PercentageType>")
            Builder.Append("<Observations>" & detail.Observations & "</Observations>")
            Builder.Append("<InitialDate>" & detail.InitialDate.ToString("dd/MM/yyyy HH:mm") & "</InitialDate>")
            Builder.Append("<FinalDate>" & detail.FinalDate.ToString("dd/MM/yyyy HH:mm") & "</FinalDate>")
            Builder.Append("<SalePrice>" & detail.SalePrice.ToString().Replace(",", ".") & "</SalePrice>")
            builder.Append("<Percentage>" & detail.Percentage.ToString().Replace(",", ".") & "</Percentage>")
            builder.Append("<IsDelete>" & If(detail.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")

            builder.Append("</ProductFeeDetail>")
            rowProductFeeDetail += 1
        Next

        For Each detail In ProductAndServiceFee.ServiceFeeDetail
            builder.Append("<ServiceFeeDetail>")

            builder.Append("<Id>" & detail.Id & "</Id>")
            builder.Append("<TempId>" & rowServiceFeeDetail & "</TempId>")
            builder.Append("<ProductAndServiceFeeId>" & detail.ProductAndServiceFeeId & "</ProductAndServiceFeeId>")
            builder.Append("<ServiceId>" & detail.ServiceId & "</ServiceId>")
            builder.Append("<Observations>" & detail.Observations & "</Observations>")
            builder.Append("<InitialDate>" & detail.InitialDate & "</InitialDate>")
            builder.Append("<FinalDate>" & detail.FinalDate & "</FinalDate>")
            builder.Append("<SalePrice>" & detail.SalePrice.ToString().Replace(",", ".") & "</SalePrice>")
            builder.Append("<IsDelete>" & If(detail.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")

            builder.Append("</ServiceFeeDetail>")
            rowServiceFeeDetail += 1
        Next

        For Each detail In ProductAndServiceFee.ProductAndServiceFeeUser
            Builder.Append("<ProductAndServiceFeeUser>")

            Builder.Append("<Id>" & detail.Id & "</Id>")
            Builder.Append("<TempId>" & rowUserDetail & "</TempId>")
            Builder.Append("<UserId>" & detail.UserId & "</UserId>")
            Builder.Append("<UserCode>" & detail.UserCode & "</UserCode>")
            Builder.Append("<IsDelete>" & If(detail.ChangeTracker.State = ObjectState.Deleted, 1, 0) & "</IsDelete>")

            Builder.Append("</ProductAndServiceFeeUser>")
            rowUserDetail += 1
        Next

        Builder.Append("</ProductAndServiceFee>")

        Return builder.ToString()
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then

            'Limpiar
            Me._ProductAndServiceFeeRepository = Nothing

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
