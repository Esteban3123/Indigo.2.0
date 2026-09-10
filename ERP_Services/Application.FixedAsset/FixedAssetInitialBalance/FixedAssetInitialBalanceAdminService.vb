'************************************************************
' Assembly         : Application.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/04/2016
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Data.Entity.Core
Imports System.Text
Imports System.Transactions
Imports Application.FixedAsset
Imports System.Data.SqlClient

#End Region

Public Class FixedAssetInitialBalanceAdminService
    Implements IFixedAssetInitialBalanceAdminService

#Region "Variables"

    'Repositorio de el tipo de equipo
    Private _fixedAssetInitialBalanceRepository As IFixedAssetInitialBalanceRepository

    'repositorio de la secuencia
    Private _sequenceRepository As IFixedAssetSequenceDetailRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' inicia el repositorio de tipo de equipo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal fixedAssetInitialBalanceRepository As IFixedAssetInitialBalanceRepository, sequenceRepository As IFixedAssetSequenceDetailRepository)
        If fixedAssetInitialBalanceRepository Is Nothing Then
            Throw New ArgumentNullException("fixedAssetInitialBalanceRepository")
        End If
        If sequenceRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceRepository")
        End If
        _fixedAssetInitialBalanceRepository = fixedAssetInitialBalanceRepository
        _sequenceRepository = sequenceRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene el saldo inicial por código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetInitialBalance(Code As String, audit As AuditMessage) As ActionResult(Of FixedAssetInitialBalance) Implements IFixedAssetInitialBalanceAdminService.GetFixedAssetInitialBalance
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim FixedAssetInitialBalance As FixedAssetInitialBalance = Me._fixedAssetInitialBalanceRepository.GetFixedAssetInitialBalance(Code.Trim())
            If FixedAssetInitialBalance IsNot Nothing AndAlso FixedAssetInitialBalance.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetInitialBalance)(FixedAssetInitialBalance, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of FixedAssetInitialBalance) With {.StateResult = True, .ObjectEmbbeded = FixedAssetInitialBalance}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetInitialBalance) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene saldo inicial por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetInitialBalanceById(Id As Integer, audit As AuditMessage) As ActionResult(Of FixedAssetInitialBalance) Implements IFixedAssetInitialBalanceAdminService.GetFixedAssetInitialBalanceById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim FixedAssetInitialBalance As FixedAssetInitialBalance = Me._fixedAssetInitialBalanceRepository.GetFixedAssetInitialBalanceById(Id)
            If FixedAssetInitialBalance IsNot Nothing AndAlso FixedAssetInitialBalance.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetInitialBalance)(FixedAssetInitialBalance, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of FixedAssetInitialBalance) With {.StateResult = True, .ObjectEmbbeded = FixedAssetInitialBalance}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetInitialBalance) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda un saldo inicial
    ''' </summary>
    ''' <param name="FixedAssetInitialBalance"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveFixedAssetInitialBalance(FixedAssetInitialBalance As FixedAssetInitialBalance, ListDeleteFixedAssetInitialBalanceItem As List(Of FixedAssetInitialBalanceItem), ListDeleteFixedAssetInitialBalanceItemPartsDetailBook As List(Of FixedAssetInitialBalanceItemPartsDetailBook), ListDeleteFixedAssetInitialBalanceItemDetailBook As List(Of FixedAssetInitialBalanceItemDetailBook), ListDeleteFixedAssetInitialBalanceItemParts As List(Of FixedAssetInitialBalanceItemParts), audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetInitialBalance) Implements IFixedAssetInitialBalanceAdminService.SaveFixedAssetInitialBalance
        If FixedAssetInitialBalance Is Nothing Then
            Throw New ArgumentNullException("FixedAssetInitialBalance")
        End If
        Dim unitOfWork As IUnitWork = Me._fixedAssetInitialBalanceRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try

                If FixedAssetInitialBalance.Status <> 3 Then
                    Dim ListString As List(Of String) = ConvertToXmlListDeletes(ListDeleteFixedAssetInitialBalanceItem, ListDeleteFixedAssetInitialBalanceItemPartsDetailBook, ListDeleteFixedAssetInitialBalanceItemDetailBook, ListDeleteFixedAssetInitialBalanceItemParts)
                    Dim resultStore = _fixedAssetInitialBalanceRepository.SP_SaveFixedAssetInitialBalance(ListString, audit.CodeUser)
                    If resultStore.CodeMessage <> 0 Then
                        transaction.Dispose()
                        Return New ActionResult(Of FixedAssetInitialBalance) With {.StateResult = False, .MessageResult = {resultStore.Message}.ToList}
                    End If
                End If

                Dim seq As FixedAssetSequenceDetail = Nothing
                If FixedAssetInitialBalance.Code Is Nothing OrElse FixedAssetInitialBalance.Code.Trim().Equals(String.Empty) Then
                    seq = Me._sequenceRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            FixedAssetInitialBalance.Code = res
                            seq.Next += 1
                            Me._sequenceRepository.SaveEntity(seq)
                        Else
                            transaction.Dispose()
                            Return New ActionResult(Of FixedAssetInitialBalance) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                        End If
                    Else
                        transaction.Dispose()
                        Return New ActionResult(Of FixedAssetInitialBalance) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                    End If
                End If

                Dim auxFixedAssetInitialBalance As FixedAssetInitialBalance = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of FixedAssetInitialBalance)
                Dim status As Integer

                If FixedAssetInitialBalance.ChangeTracker.State = ObjectState.Added Then
                    FixedAssetInitialBalance.CreationUser = audit.CodeUser
                    FixedAssetInitialBalance.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                ElseIf FixedAssetInitialBalance.ChangeTracker.State = ObjectState.Modified Then
                    auxFixedAssetInitialBalance = FixedAssetInitialBalance.OriginalValue
                    If FixedAssetInitialBalance.Status = 1 Then
                        FixedAssetInitialBalance.ModificationUser = audit.CodeUser
                        FixedAssetInitialBalance.ModificationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Update
                    End If
                    If FixedAssetInitialBalance.Status = 2 Then
                        FixedAssetInitialBalance.ModificationUser = audit.CodeUser
                        FixedAssetInitialBalance.ModificationDate = DateTime.Now
                        FixedAssetInitialBalance.ConfirmationUser = audit.CodeUser
                        FixedAssetInitialBalance.ConfirmationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Confirm
                    End If
                    If FixedAssetInitialBalance.Status = 3 Then
                        FixedAssetInitialBalance.ModificationUser = audit.CodeUser
                        FixedAssetInitialBalance.ModificationDate = DateTime.Now
                        FixedAssetInitialBalance.AnnulmentUser = audit.CodeUser
                        FixedAssetInitialBalance.AnnulmentDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Annular
                    End If
                End If

                Me._fixedAssetInitialBalanceRepository.SaveEntity(FixedAssetInitialBalance)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of FixedAssetInitialBalance)(FixedAssetInitialBalance, audit, status, auxFixedAssetInitialBalance)
                auditProcess.Execute()

                'Si se está confirmando el saldo inicial se inserta en las tablas de physicalAsset con el sp, se realiza despues del saveEntity porque se necesita el id que genera el saldo inicial
                If FixedAssetInitialBalance.Status = 2 Then
                    Dim resultConfirm = _fixedAssetInitialBalanceRepository.SP_ConfirmFixedAssetInitialBalance(FixedAssetInitialBalance.Id)
                    If resultConfirm.CodeMessage <> 0 Then
                        unitOfWork.RollbackChanges()
                        transaction.Dispose()
                        Return New ActionResult(Of FixedAssetInitialBalance) With {.StateResult = False, .MessageResult = {resultConfirm.Message}.ToList}
                    End If
                End If

                'Se marca la entidad como sin cambios
                FixedAssetInitialBalance.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of FixedAssetInitialBalance) With {.StateResult = True, .ObjectEmbbeded = FixedAssetInitialBalance}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of FixedAssetInitialBalance) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of FixedAssetInitialBalance) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Convertir el objeto en xml
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertToXml(FixedAssetInitialBalance As FixedAssetInitialBalance) As String
        Dim builder As StringBuilder = New StringBuilder()

        Dim ContFixedAssetInitialBalanceItem = 1
        Dim ContFixedAssetInitialBalanceItemParts = 1

        'FixedAssetInitialBalance
        builder.Append("<FixedAssetInitialBalance>")

        builder.Append("<Id>" & FixedAssetInitialBalance.Id & "</Id>")
        builder.Append("<OperatingUnitId>" & FixedAssetInitialBalance.OperatingUnitId & "</OperatingUnitId>")
        builder.Append("<Code>" & FixedAssetInitialBalance.Code & "</Code>")
        builder.Append("<DocumentDate>" & FixedAssetInitialBalance.DocumentDate & "</DocumentDate>")
        builder.Append("<Observations>" & FixedAssetInitialBalance.Observations & "</Observations>")
        builder.Append("<Status>" & FixedAssetInitialBalance.Status & "</Status>")

        For Each item In FixedAssetInitialBalance.FixedAssetInitialBalanceItem

            'FixedAssetInitialBalanceItem
            builder.Append("<FixedAssetInitialBalanceItem>")

            builder.Append("<FixedAssetInitialBalanceId>" & item.FixedAssetInitialBalanceId & "</FixedAssetInitialBalanceId>")
            builder.Append("<ItemId>" & item.ItemId & "</ItemId>")
            builder.Append("<Serie>" & item.Serie & "</Serie>")
            builder.Append("<Plate>" & item.Plate & "</Plate>")
            builder.Append("<LocationId>" & item.LocationId & "</LocationId>")
            builder.Append("<ResponsibleId>" & item.ResponsibleId & "</ResponsibleId>")
            builder.Append("<SupplierId>" & item.SupplierId & "</SupplierId>")
            builder.Append("<HistoricalValue>" & item.HistoricalValue & "</HistoricalValue>")
            builder.Append("<FairValue>" & item.FairValue & "</FairValue>")
            builder.Append("<TrademarkId>" & item.TrademarkId & "</TrademarkId>")
            builder.Append("<Model>" & item.Model & "</Model>")
            builder.Append("<PolicyId>" & item.PolicyId & "</PolicyId>")
            builder.Append("<HandlesWarranty>" & item.HandlesWarranty & "</HandlesWarranty>")
            builder.Append("<WarrantyExpirationDate>" & item.WarrantyExpirationDate & "</WarrantyExpirationDate>")
            builder.Append("<AdquisitionDate>" & item.AdquisitionDate & "</AdquisitionDate>")
            builder.Append("<Depreciate>" & item.Depreciate & "</Depreciate>")
            builder.Append("<ChangeTracker>" & 0 & "</ChangeTracker>")
            builder.Append("<TempId>" & ContFixedAssetInitialBalanceItem & "</TempId>")

            If item.FixedAssetInitialBalanceItemDetailBook IsNot Nothing AndAlso item.FixedAssetInitialBalanceItemDetailBook.Count > 0 Then
                For Each itemDB In item.FixedAssetInitialBalanceItemDetailBook

                    'FixedAssetInitialBalanceItemDetailBook
                    builder.Append("<FixedAssetInitialBalanceItemDetailBook>")

                    builder.Append("<FixedAssetInitialBalanceItemId>" & itemDB.FixedAssetInitialBalanceItemId & "</FixedAssetInitialBalanceItemId>")
                    builder.Append("<LegalBookId>" & itemDB.LegalBookId & "</LegalBookId>")
                    builder.Append("<LifeTime>" & itemDB.LifeTime & "</LifeTime>")
                    builder.Append("<UnitLifeTime>" & itemDB.UnitLifeTime & "</UnitLifeTime>")
                    builder.Append("<DepreciatedDays>" & itemDB.DepreciatedDays & "</DepreciatedDays>")
                    builder.Append("<DepreciationType>" & itemDB.DepreciationType & "</DepreciationType>")
                    builder.Append("<TotalProductionUnit>" & itemDB.TotalProductionUnit & "</TotalProductionUnit>")
                    builder.Append("<PercentageRescue>" & itemDB.PercentageRescue & "</PercentageRescue>")
                    builder.Append("<DepreciatedValue>" & itemDB.DepreciatedValue & "</DepreciatedValue>")
                    builder.Append("<DepreciatedValue>" & itemDB.DepreciatedValue & "</DepreciatedValue>")
                    builder.Append("<ChangeTracker>" & 0 & "</ChangeTracker>")
                    builder.Append("<FixedAssetInitialBalanceItemTempId>" & ContFixedAssetInitialBalanceItem & "</FixedAssetInitialBalanceItemTempId>")

                    builder.Append("</FixedAssetInitialBalanceItemDetailBook>")

                Next
            End If

            If item.FixedAssetInitialBalanceItemParts IsNot Nothing AndAlso item.FixedAssetInitialBalanceItemParts.Count > 0 Then
                For Each itemP In item.FixedAssetInitialBalanceItemParts

                    'FixedAssetInitialBalanceItemParts
                    builder.Append("<FixedAssetInitialBalanceItemParts>")

                    builder.Append("<FixedAssetInitialBalanceItemId>" & itemP.FixedAssetInitialBalanceItemId & "</FixedAssetInitialBalanceItemId>")
                    builder.Append("<PartAccesoriesConsumablesId>" & itemP.PartAccesoriesConsumablesId & "</PartAccesoriesConsumablesId>")
                    builder.Append("<DepreciatePart>" & itemP.DepreciatePart & "</DepreciatePart>")
                    builder.Append("<HistoricalValue>" & itemP.HistoricalValue & "</HistoricalValue>")
                    builder.Append("<ChangeTracker>" & 0 & "</ChangeTracker>")
                    builder.Append("<FixedAssetInitialBalanceItemTempId>" & ContFixedAssetInitialBalanceItem & "</FixedAssetInitialBalanceItemTempId>")
                    builder.Append("<TempId>" & ContFixedAssetInitialBalanceItemParts & "</TempId>")

                    If itemP.FixedAssetInitialBalanceItemPartsDetailBook IsNot Nothing AndAlso itemP.FixedAssetInitialBalanceItemPartsDetailBook.Count > 0 Then
                        For Each itemPDB In itemP.FixedAssetInitialBalanceItemPartsDetailBook

                            'FixedAssetInitialBalanceItemPartsDetailBook
                            builder.Append("<FixedAssetInitialBalanceItemPartsDetailBook>")

                            builder.Append("<FixedAssetInitialBalanceItemPartsId>" & itemPDB.FixedAssetInitialBalanceItemPartsId & "</FixedAssetInitialBalanceItemPartsId>")
                            builder.Append("<LegalBookId>" & itemPDB.LegalBookId & "</LegalBookId>")
                            builder.Append("<LifeTime>" & itemPDB.LifeTime & "</LifeTime>")
                            builder.Append("<UnitLifeTime>" & itemPDB.UnitLifeTime & "</UnitLifeTime>")
                            builder.Append("<DepreciatedDays>" & itemPDB.DepreciatedDays & "</DepreciatedDays>")
                            builder.Append("<DepreciationType>" & itemPDB.DepreciationType & "</DepreciationType>")
                            builder.Append("<TotalProductionUnit>" & itemPDB.TotalProductionUnit & "</TotalProductionUnit>")
                            builder.Append("<PercentageRescue>" & itemPDB.PercentageRescue & "</PercentageRescue>")
                            builder.Append("<DepreciatedValue>" & itemPDB.DepreciatedValue & "</DepreciatedValue>")
                            builder.Append("<ChangeTracker>" & 0 & "</ChangeTracker>")
                            builder.Append("<FixedAssetInitialBalanceItemPartsTempId>" & ContFixedAssetInitialBalanceItemParts & "</FixedAssetInitialBalanceItemPartsTempId>")

                            builder.Append("</FixedAssetInitialBalanceItemPartsDetailBook>")

                        Next
                    End If

                    ContFixedAssetInitialBalanceItemParts += 1

                    builder.Append("</FixedAssetInitialBalanceItemParts>")

                Next
            End If

            ContFixedAssetInitialBalanceItem += 1

            builder.Append("</FixedAssetInitialBalanceItem>")

        Next

        builder.Append("</FixedAssetInitialBalance>")

        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Convierte a objeto xml a todos los listados de eliminados
    ''' </summary>
    ''' <param name="ListDeleteFixedAssetInitialBalanceItem"></param>
    ''' <param name="ListDeleteFixedAssetInitialBalanceItemPartsDetailBook"></param>
    ''' <param name="ListDeleteFixedAssetInitialBalanceItemDetailBook"></param>
    ''' <param name="ListDeleteFixedAssetInitialBalanceItemParts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertToXmlListDeletes(ListDeleteFixedAssetInitialBalanceItem As List(Of FixedAssetInitialBalanceItem), ListDeleteFixedAssetInitialBalanceItemPartsDetailBook As List(Of FixedAssetInitialBalanceItemPartsDetailBook), ListDeleteFixedAssetInitialBalanceItemDetailBook As List(Of FixedAssetInitialBalanceItemDetailBook), ListDeleteFixedAssetInitialBalanceItemParts As List(Of FixedAssetInitialBalanceItemParts)) As List(Of String)
        Dim builder As StringBuilder
        Dim ListString As New List(Of String)

        builder = New StringBuilder()
        If ListDeleteFixedAssetInitialBalanceItemPartsDetailBook IsNot Nothing AndAlso ListDeleteFixedAssetInitialBalanceItemPartsDetailBook.Count > 0 Then
            For Each item In ListDeleteFixedAssetInitialBalanceItemPartsDetailBook
                builder.Append("<ListDeleteFixedAssetInitialBalanceItemPartsDetailBook>")
                builder.Append("<Id>" & item.Id & "</Id>")
                builder.Append("</ListDeleteFixedAssetInitialBalanceItemPartsDetailBook>")
            Next
        Else
            builder.Append("<ListDeleteFixedAssetInitialBalanceItemPartsDetailBook>")
            builder.Append("<Id>" & 0 & "</Id>")
            builder.Append("</ListDeleteFixedAssetInitialBalanceItemPartsDetailBook>")
        End If
        ListString.Add(builder.ToString)

        builder = New StringBuilder()
        If ListDeleteFixedAssetInitialBalanceItemParts IsNot Nothing AndAlso ListDeleteFixedAssetInitialBalanceItemParts.Count > 0 Then
            For Each item In ListDeleteFixedAssetInitialBalanceItemParts
                builder.Append("<ListDeleteFixedAssetInitialBalanceItemParts>")
                builder.Append("<Id>" & item.Id & "</Id>")
                builder.Append("</ListDeleteFixedAssetInitialBalanceItemParts>")
            Next
        Else
            builder.Append("<ListDeleteFixedAssetInitialBalanceItemParts>")
            builder.Append("<Id>" & 0 & "</Id>")
            builder.Append("</ListDeleteFixedAssetInitialBalanceItemParts>")
        End If
        ListString.Add(builder.ToString)

        builder = New StringBuilder()
        If ListDeleteFixedAssetInitialBalanceItemDetailBook IsNot Nothing AndAlso ListDeleteFixedAssetInitialBalanceItemDetailBook.Count > 0 Then
            For Each item In ListDeleteFixedAssetInitialBalanceItemDetailBook
                builder.Append("<ListDeleteFixedAssetInitialBalanceItemDetailBook>")
                builder.Append("<Id>" & item.Id & "</Id>")
                builder.Append("</ListDeleteFixedAssetInitialBalanceItemDetailBook>")
            Next
        Else
            builder.Append("<ListDeleteFixedAssetInitialBalanceItemDetailBook>")
            builder.Append("<Id>" & 0 & "</Id>")
            builder.Append("</ListDeleteFixedAssetInitialBalanceItemDetailBook>")
        End If
        ListString.Add(builder.ToString)

        builder = New StringBuilder()
        If ListDeleteFixedAssetInitialBalanceItem IsNot Nothing AndAlso ListDeleteFixedAssetInitialBalanceItem.Count > 0 Then
            For Each item In ListDeleteFixedAssetInitialBalanceItem
                builder.Append("<ListDeleteFixedAssetInitialBalanceItem>")
                builder.Append("<Id>" & item.Id & "</Id>")
                builder.Append("</ListDeleteFixedAssetInitialBalanceItem>")
            Next
        Else
            builder.Append("<ListDeleteFixedAssetInitialBalanceItem>")
            builder.Append("<Id>" & 0 & "</Id>")
            builder.Append("</ListDeleteFixedAssetInitialBalanceItem>")
        End If
        ListString.Add(builder.ToString)

        '(0): ListDeleteFixedAssetInitialBalanceItemPartsDetailBook
        '(1): ListDeleteFixedAssetInitialBalanceItemParts
        '(2): ListDeleteFixedAssetInitialBalanceItemDetailBook
        '(3): ListDeleteFixedAssetInitialBalanceItem
        Return ListString
    End Function

    ''' <summary>
    ''' Copiar y pegar de saldo inicial de activos fijos
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function SP_CopyAndPasteFixedAssetInitialBalance(data As List(Of List(Of String))) As ActionResult(Of List(Of FixedAssetInitialBalanceItem), List(Of Tuple(Of String, Integer))) Implements IFixedAssetInitialBalanceAdminService.SP_CopyAndPasteFixedAssetInitialBalance
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If

        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListFixedAssetInitialBalanceItem As New List(Of FixedAssetInitialBalanceItem)

        'Listado de errores
        Dim listErrors As New List(Of Tuple(Of String, Integer))
        Try
            'Objeto xml
            Dim xmlObject = ConvertToXml(data)

            'Se consume el procedimiento almacenado
            Dim resultStore = _fixedAssetInitialBalanceRepository.SP_CopyAndPasteFixedAssetInitialBalance(xmlObject)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then

                'Listado agrupado por activo
                Dim ListGroupBy = From x In resultStore
                                  Group x By x.ItemId, x.ItemDescription, x.Serie, x.Plate, x.ResponsibleId, x.ResponsibleDescription, x.HistoricalValue,
                                      x.FairValue, x.TrademarkId, x.TrademarkDescription, x.Model, x.SupplierId, x.SupplierDescription, x.LocationId, x.LocationDescription,
                                      x.PolicyId, x.PolicyDescription, x.HandlesWarranty, x.WarrantyExpirationDate, x.AdquisitionDate, x.AdquisitionType,
                                      x.Depreciate, x.StatusAssetId, x.StatusAssetDescription, x.ValidMinorAmount, x.Amortize
                                      Into ResultGroup = Group

                'Se recorre el listado agrupado
                For Each itemXml In ListGroupBy

                    'Se obtienen los mensajes de error de cada item
                    itemXml.ResultGroup.Where(Function(x) x.StatusField = 0).ToList().ForEach(Sub(y) listErrors.Add(New Tuple(Of String, Integer)(y.MessageField, 2)))

                    'Si alguno de los libros arroja un error no se agrega el articulo como tal
                    If (From x In itemXml.ResultGroup Where x.StatusField = 0 Select x).Count > 0 Then
                        Continue For
                    End If

                    'Se crea la entidad para agregar al listado
                    Dim FixedAssetInitialBalanceItem = New FixedAssetInitialBalanceItem
                    With FixedAssetInitialBalanceItem
                        .ItemId = itemXml.ItemId
                        .ItemCodeName = itemXml.ItemDescription
                        .Serie = itemXml.Serie
                        .Plate = itemXml.Plate
                        .LocationId = itemXml.LocationId
                        .LocationCodeName = itemXml.LocationDescription
                        .ResponsibleId = itemXml.ResponsibleId
                        .ResponsibleCodeName = itemXml.ResponsibleDescription
                        .SupplierId = itemXml.SupplierId
                        .SupplierCodeName = itemXml.SupplierDescription
                        .HistoricalValue = itemXml.HistoricalValue
                        .FairValue = itemXml.FairValue
                        .TrademarkId = itemXml.TrademarkId
                        .TrademarkCodeName = itemXml.TrademarkDescription
                        .Model = itemXml.Model
                        .PolicyId = itemXml.PolicyId
                        .PolicyCodeName = itemXml.PolicyDescription
                        .HandlesWarranty = itemXml.HandlesWarranty
                        If .HandlesWarranty Then 'Si maneja garantía
                            .WarrantyExpirationDate = itemXml.WarrantyExpirationDate
                        Else 'Si no maneja garantía
                            .WarrantyExpirationDate = Nothing
                        End If
                        .AdquisitionDate = itemXml.AdquisitionDate
                        .Depreciate = itemXml.Depreciate
                        .AdquisitionType = itemXml.AdquisitionType
                        .StatusAssetId = itemXml.StatusAssetId
                        .StatusAssetCodeName = itemXml.StatusAssetDescription
                        .Status = True
                        If itemXml.ValidMinorAmount Is Nothing Then
                            .ValidMinorAmount = New Nullable(Of Boolean)
                        Else
                            .ValidMinorAmount = itemXml.ValidMinorAmount
                        End If
                        .Amortize = itemXml.Amortize
                    End With

                    'Se generan los detalles
                    itemXml.ResultGroup.Where(Function(x) x.StatusField = 1).ToList() _
                        .ForEach(Sub(y)
                                     Dim FixedAssetInitialBalanceItemDetailBook = New FixedAssetInitialBalanceItemDetailBook
                                     With FixedAssetInitialBalanceItemDetailBook
                                         .LegalBookId = y.BookId
                                         .LegalBookCodeName = y.BookDescription
                                         .LifeTime = y.LifeTime
                                         .UnitLifeTime = y.UnitLifeTime
                                         .DaysPendingDepreciate = IIf(y.UnitLifeTime = 1, y.LifeTime * 360, IIf(y.UnitLifeTime = 2, y.LifeTime * 30, y.LifeTime)) - y.DepreciatedDays
                                         .DepreciatedDays = y.DepreciatedDays
                                         .DepreciationType = y.DepreciationType
                                         .TotalProductionUnit = 0
                                         .PercentageRescue = 0
                                         .DepreciatedValue = y.DepreciatedValue
                                         .DepreciatedValuePart = 0
                                         .ResidualValue = y.HistoricalValueInBook - y.DepreciatedValue
                                         .ResidualValuePart = 0
                                         .HistoricalValue = y.HistoricalValueInBook
                                     End With
                                     FixedAssetInitialBalanceItem.FixedAssetInitialBalanceItemDetailBook.Add(FixedAssetInitialBalanceItemDetailBook)
                                 End Sub)

                    'Se agrega al listado que se devuelve
                    ListFixedAssetInitialBalanceItem.Add(FixedAssetInitialBalanceItem)
                Next
            End If


            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of FixedAssetInitialBalanceItem), List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = ListFixedAssetInitialBalanceItem, .ObjectEmbbededAux = listErrors, .StateResult = True}
        Catch ex As SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of FixedAssetInitialBalanceItem), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of FixedAssetInitialBalanceItem), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of FixedAssetInitialBalanceItem), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

    ''' <summary>
    ''' Convierte el listado de datos a xml
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertToXml(data As List(Of List(Of String)))
        'String para devolver
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        'Cantidad de item para validar 
        Dim count As Integer = 0

        For Each item In data
            'Se asigna la cantidad de items
            count = item.Count

            builder.Append("<Row>")

            builder.Append("<CountFields>" & item.Count & "</CountFields>")
            builder.Append("<StatusField>" & 0 & "</StatusField>")
            builder.Append("<MessageField>" & "" & "</MessageField>")

            If count > 0 Then
                builder.Append("<ItemCode>" & item(0) & "</ItemCode>")
                count -= 1
            Else
                builder.Append("<ItemCode></ItemCode>")
            End If
            builder.Append("<ItemDescription>" & "---" & "</ItemDescription>")
            builder.Append("<ItemId>" & 0 & "</ItemId>")

            If count > 0 Then
                builder.Append("<Serie>" & item(1) & "</Serie>")
                count -= 1
            Else
                builder.Append("<Serie></Serie>")
            End If

            If count > 0 Then
                builder.Append("<Plate>" & item(2) & "</Plate>")
                count -= 1
            Else
                builder.Append("<Plate></Plate>")
            End If

            If count > 0 Then
                builder.Append("<ResponsibleCode>" & item(3) & "</ResponsibleCode>")
                count -= 1
            Else
                builder.Append("<ResponsibleCode></ResponsibleCode>")
            End If
            builder.Append("<ResponsibleDescription>" & "---" & "</ResponsibleDescription>")
            builder.Append("<ResponsibleId>" & 0 & "</ResponsibleId>")

            If count > 0 Then
                builder.Append("<HistoricalValue>" & item(4) & "</HistoricalValue>")
                count -= 1
            Else
                builder.Append("<HistoricalValue></HistoricalValue>")
            End If

            If count > 0 Then
                builder.Append("<FairValue>" & item(5) & "</FairValue>")
                count -= 1
            Else
                builder.Append("<FairValue></FairValue>")
            End If

            If count > 0 Then
                builder.Append("<TrademarkCode>" & item(6) & "</TrademarkCode>")
                count -= 1
            Else
                builder.Append("<TrademarkCode></TrademarkCode>")
            End If
            builder.Append("<TrademarkDescription>" & "---" & "</TrademarkDescription>")
            builder.Append("<TrademarkId>" & 0 & "</TrademarkId>")

            If count > 0 Then
                builder.Append("<Model>" & item(7) & "</Model>")
                count -= 1
            Else
                builder.Append("<Model></Model>")
            End If

            If count > 0 Then
                builder.Append("<SupplierCode>" & item(8) & "</SupplierCode>")
                count -= 1
            Else
                builder.Append("<SupplierCode></SupplierCode>")
            End If
            builder.Append("<SupplierDescription>" & "---" & "</SupplierDescription>")
            builder.Append("<SupplierId>" & 0 & "</SupplierId>")

            If count > 0 Then
                builder.Append("<LocationCode>" & item(9) & "</LocationCode>")
                count -= 1
            Else
                builder.Append("<LocationCode></LocationCode>")
            End If
            builder.Append("<LocationDescription>" & "---" & "</LocationDescription>")
            builder.Append("<LocationId>" & 0 & "</LocationId>")

            If count > 0 Then
                builder.Append("<PolicyCode>" & item(10) & "</PolicyCode>")
                count -= 1
            Else
                builder.Append("<PolicyCode></PolicyCode>")
            End If
            builder.Append("<PolicyDescription>" & "---" & "</PolicyDescription>")
            builder.Append("<PolicyId>" & 0 & "</PolicyId>")

            If count > 0 Then
                builder.Append("<HandlesWarranty>" & item(11) & "</HandlesWarranty>")
                count -= 1
            Else
                builder.Append("<HandlesWarranty></HandlesWarranty>")
            End If

            If count > 0 Then
                builder.Append("<WarrantyExpirationDate>" & item(12) & "</WarrantyExpirationDate>")
                count -= 1
            Else
                builder.Append("<WarrantyExpirationDate></WarrantyExpirationDate>")
            End If

            If count > 0 Then
                builder.Append("<AdquisitionDate>" & item(13) & "</AdquisitionDate>")
                count -= 1
            Else
                builder.Append("<AdquisitionDate></AdquisitionDate>")
            End If

            If count > 0 Then
                builder.Append("<AdquisitionType>" & item(14) & "</AdquisitionType>")
                count -= 1
            Else
                builder.Append("<AdquisitionType></AdquisitionType>")
            End If

            If count > 0 Then
                builder.Append("<Depreciate>" & item(15) & "</Depreciate>")
                count -= 1
            Else
                builder.Append("<Depreciate></Depreciate>")
            End If


            If count > 0 Then
                builder.Append("<ValidMinorAmount>" & item(16) & "</ValidMinorAmount>")
                count -= 1
            Else
                builder.Append("<ValidMinorAmount></ValidMinorAmount>")
            End If

            If count > 0 Then
                builder.Append("<StatusAssetCode>" & item(17) & "</StatusAssetCode>")
                count -= 1
            Else
                builder.Append("<StatusAssetCode></StatusAssetCode>")
            End If
            builder.Append("<StatusAssetDescription>" & "---" & "</StatusAssetDescription>")
            builder.Append("<StatusAssetId>" & 0 & "</StatusAssetId>")

            If count > 0 Then
                builder.Append("<BookCode>" & item(18) & "</BookCode>")
                count -= 1
            Else
                builder.Append("<BookCode></BookCode>")
            End If
            builder.Append("<BookDescription>" & "---" & "</BookDescription>")
            builder.Append("<BookId>" & 0 & "</BookId>")

            If count > 0 Then
                builder.Append("<LifeTime>" & item(19) & "</LifeTime>")
                count -= 1
            Else
                builder.Append("<LifeTime></LifeTime>")
            End If

            If count > 0 Then
                builder.Append("<UnitLifeTime>" & item(20) & "</UnitLifeTime>")
                count -= 1
            Else
                builder.Append("<UnitLifeTime></UnitLifeTime>")
            End If

            If count > 0 Then
                builder.Append("<DepreciationType>" & item(21) & "</DepreciationType>")
                count -= 1
            Else
                builder.Append("<DepreciationType></DepreciationType>")
            End If

            If count > 0 Then
                builder.Append("<DepreciatedDays>" & item(22) & "</DepreciatedDays>")
                count -= 1
            Else
                builder.Append("<DepreciatedDays></DepreciatedDays>")
            End If

            If count > 0 Then
                builder.Append("<DepreciatedValue>" & item(23) & "</DepreciatedValue>")
                count -= 1
            Else
                builder.Append("<DepreciatedValue></DepreciatedValue>")
            End If

            If count > 0 Then
                builder.Append("<HistoricalValueInBook>" & item(24) & "</HistoricalValueInBook>")
                count -= 1
            Else
                builder.Append("<HistoricalValueInBook></HistoricalValueInBook>")
            End If

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _sequenceRepository = Nothing
            _fixedAssetInitialBalanceRepository = Nothing
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
