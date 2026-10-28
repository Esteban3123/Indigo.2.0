Imports System.IO
Imports System.Runtime.Serialization
Imports System.Text
Imports Application.Accounting
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports NUnit.Framework
Imports System.Data.Entity.Infrastructure
Imports System.Linq.Expressions
Imports System.Threading.Tasks

''' <summary>
''' Pruebas aisladas de persistencia de AppliesCatalogPropertyandServices.
''' No requieren ERP ni base de datos compartida.
''' </summary>
<TestFixture>
Public Class CompanySettingsAppliesCatalogPersistenceTests

    <Test>
    Public Sub DataContract_Roundtrip_PreservesAppliesCatalogTrue()
        Dim original As New CompanySettings()
        original.Id = 1
        original.AppliesCatalogPropertyandServices = True
        original.TransactionEconomicActivity = False
        original.MarkAsUnchanged()

        Dim deserialized = Roundtrip(original)

        Assert.IsTrue(deserialized.AppliesCatalogPropertyandServices)
        Assert.IsFalse(deserialized.TransactionEconomicActivity)
        Assert.AreEqual(CByte(1), deserialized.Id)
    End Sub

    <Test>
    Public Sub DataContract_Roundtrip_PreservesAppliesCatalogFalse()
        Dim original As New CompanySettings()
        original.Id = 1
        original.AppliesCatalogPropertyandServices = True
        original.MarkAsUnchanged()
        original.StartTracking()
        original.AppliesCatalogPropertyandServices = False

        Dim deserialized = Roundtrip(original)

        Assert.IsFalse(deserialized.AppliesCatalogPropertyandServices)
    End Sub

    <Test>
    Public Sub SaveCompanySettings_WhenCatalogIsTrue_PersistsValueOnEntity()
        Dim unitOfWork As New FakeUnitOfWork()
        Dim repository As New FakeCompanySettingsRepository(unitOfWork)
        Dim service As New CompanySettingsAdminService(repository)
        Dim settings As New CompanySettings()
        settings.Id = 1
        settings.LastClosingDate = DateTime.Today
        settings.MarkAsUnchanged()
        settings.StartTracking()
        settings.AppliesCatalogPropertyandServices = True

        Dim result = service.SaveCompanySettings(settings, New AuditMessage() With {.CodeUser = "TEST"})

        Assert.IsTrue(result.StateResult)
        Assert.IsTrue(repository.LastSaved.AppliesCatalogPropertyandServices)
        Assert.AreEqual(1, unitOfWork.CommitCount)
    End Sub

    <Test>
    Public Sub SaveCompanySettings_WhenOtherParameterChanges_DoesNotClearCatalog()
        Dim unitOfWork As New FakeUnitOfWork()
        Dim repository As New FakeCompanySettingsRepository(unitOfWork)
        Dim service As New CompanySettingsAdminService(repository)
        Dim settings As New CompanySettings()
        settings.Id = 1
        settings.AppliesCatalogPropertyandServices = True
        settings.SMLV = 1000D
        settings.MarkAsUnchanged()
        settings.StartTracking()
        settings.SMLV = 1300000D

        Dim result = service.SaveCompanySettings(settings, New AuditMessage() With {.CodeUser = "TEST"})

        Assert.IsTrue(result.StateResult)
        Assert.IsTrue(repository.LastSaved.AppliesCatalogPropertyandServices)
        Assert.AreEqual(1300000D, repository.LastSaved.SMLV)
    End Sub

    <Test>
    Public Sub SaveCompanySettings_WhenDbUpdateConcurrencyException_ReturnsConcurrencyCodeWithoutSuccess()
        Dim unitOfWork As New FakeUnitOfWork()
        unitOfWork.ThrowOnCommit = New DbUpdateConcurrencyException("Store update, insert, or delete statement affected an unexpected number of rows (0).")
        Dim repository As New FakeCompanySettingsRepository(unitOfWork)
        Dim service As New CompanySettingsAdminService(repository)
        Dim settings As New CompanySettings()
        settings.Id = 1
        settings.MarkAsUnchanged()
        settings.AppliesCatalogPropertyandServices = True

        Dim result = service.SaveCompanySettings(settings, New AuditMessage() With {.CodeUser = "TEST"})

        Assert.IsFalse(result.StateResult)
        Assert.AreEqual("-999", result.MessageResult(0))
        Assert.IsTrue(unitOfWork.RolledBack)
    End Sub

    <Test>
    Public Sub DbUpdateConcurrencyException_IsNotOptimisticConcurrencyException()
        Dim ex As Exception = New DbUpdateConcurrencyException("Store update, insert, or delete statement affected an unexpected number of rows (0).")
        Assert.IsFalse(TypeOf ex Is System.Data.Entity.Core.OptimisticConcurrencyException)
    End Sub

    Private Shared Function Roundtrip(settings As CompanySettings) As CompanySettings
        Dim serializer As New DataContractSerializer(GetType(CompanySettings))
        Using stream As New MemoryStream()
            serializer.WriteObject(stream, settings)
            stream.Position = 0
            Return DirectCast(serializer.ReadObject(stream), CompanySettings)
        End Using
    End Function

#Region "Fakes"

    Private Class FakeUnitOfWork
        Implements IUnitWork

        Public Property ThrowOnCommit As Exception
        Public Property CommitCount As Integer
        Public Property RolledBack As Boolean

        Public Sub Commit() Implements IUnitWork.Commit
            If ThrowOnCommit IsNot Nothing Then
                Throw ThrowOnCommit
            End If
            CommitCount += 1
        End Sub

        Public Function CommitAsync() As Task(Of Integer) Implements IUnitWork.CommitAsync
            Commit()
            Return Task.FromResult(1)
        End Function

        Public Sub CommitAndRefreshChanges() Implements IUnitWork.CommitAndRefreshChanges
            Commit()
        End Sub

        Public Sub Detach(entity As Object) Implements IUnitWork.Detach
        End Sub

        Public Sub RollbackAllChanges() Implements IUnitWork.RollbackAllChanges
            RolledBack = True
        End Sub

        Public Sub RollbackChanges() Implements IUnitWork.RollbackChanges
            RolledBack = True
        End Sub

        Public Sub RollbackChangesUnitOfWork() Implements IUnitWork.RollbackChangesUnitOfWork
            RolledBack = True
        End Sub

        Public Sub RegisterChanges(Of TEntity As {Class, IObjectWithChangeTracker})(item As TEntity) Implements IUnitWork.RegisterChanges
        End Sub

        Public Function ExecuteQueryDataSet(sqlQuery As String, nameTable As String) As DataSet Implements IUnitWork.ExecuteQueryDataSet
            Return New DataSet()
        End Function

        Public Function ExecuteQuery(Of TEntity)(sqlQuery As String, ParamArray parameters() As Object) As IEnumerable(Of TEntity) Implements ISql.ExecuteQuery
            Return Enumerable.Empty(Of TEntity)()
        End Function

        Public Function ExecuteNonQuery(sqlCommand As String, ParamArray parameters() As Object) As Integer Implements ISql.ExecuteNonQuery
            Return 0
        End Function

        Public Sub Dispose() Implements IDisposable.Dispose
        End Sub
    End Class

    Private Class FakeCompanySettingsRepository
        Implements ICompanySettingsRepository

        Private ReadOnly _unitOfWork As IUnitWork

        Public Property LastSaved As CompanySettings
        Public Property Stored As CompanySettings

        Public Sub New(unitOfWork As IUnitWork)
            _unitOfWork = unitOfWork
            Stored = New CompanySettings()
            Stored.Id = 1
            Stored.MarkAsUnchanged()
        End Sub

        Public ReadOnly Property UnitWork As IUnitWork Implements IRepository(Of CompanySettings).UnitWork
            Get
                Return _unitOfWork
            End Get
        End Property

        Public Function GetCompanySettings(Optional AsNoTracking As Boolean = False) As CompanySettings Implements ICompanySettingsRepository.GetCompanySettings
            Return Stored
        End Function

        Public Sub SaveEntity(item As CompanySettings) Implements IRepository(Of CompanySettings).SaveEntity
            LastSaved = item
            Stored = item
        End Sub

        Public Sub AddEntity(item As CompanySettings) Implements IRepository(Of CompanySettings).AddEntity
            SaveEntity(item)
        End Sub

        Public Sub DeleteEntity(item As CompanySettings) Implements IRepository(Of CompanySettings).DeleteEntity
        End Sub

        Public Sub DeleteList(item As List(Of CompanySettings)) Implements IRepository(Of CompanySettings).DeleteList
        End Sub

        Public Sub DeleteVirtual(item As CompanySettings) Implements IRepository(Of CompanySettings).DeleteVirtual
        End Sub

        Public Sub AttachEntity(item As CompanySettings) Implements IRepository(Of CompanySettings).AttachEntity
        End Sub

        Public Sub UpdateEntity(item As CompanySettings) Implements IRepository(Of CompanySettings).UpdateEntity
            SaveEntity(item)
        End Sub

        Public Function GetAll() As IEnumerable(Of CompanySettings) Implements IRepository(Of CompanySettings).GetAll
            Return New CompanySettings() {Stored}
        End Function

        Public Function SaveEntityMassiveAsync(item As List(Of CompanySettings)) As Task Implements IRepository(Of CompanySettings).SaveEntityMassiveAsync
            Return Task.CompletedTask
        End Function

        Public Function GetByFilter(filter As Expression(Of Func(Of CompanySettings, Boolean)), Optional tracking As Boolean = True, Optional includes As IEnumerable(Of String) = Nothing) As IEnumerable(Of CompanySettings) Implements IBaseRepository(Of CompanySettings).GetByFilter
            Return GetAll()
        End Function

        Public Function FirstOrDefault(filter As Expression(Of Func(Of CompanySettings, Boolean)), Optional tracking As Boolean = True, Optional includes As IEnumerable(Of String) = Nothing) As CompanySettings Implements IBaseRepository(Of CompanySettings).FirstOrDefault
            Return Stored
        End Function

        Public Function Any(filter As Expression(Of Func(Of CompanySettings, Boolean))) As Boolean Implements IBaseRepository(Of CompanySettings).Any
            Return True
        End Function

        Public Function Count(filter As Expression(Of Func(Of CompanySettings, Boolean))) As Integer Implements IBaseRepository(Of CompanySettings).Count
            Return 1
        End Function

        Public Function FindById(id As Integer) As CompanySettings Implements IBaseRepository(Of CompanySettings).FindById
            Return Stored
        End Function

        Public Function Query(filter As Expression(Of Func(Of CompanySettings, Boolean)), Optional tracking As Boolean = True, Optional includes As IEnumerable(Of String) = Nothing) As IQueryable(Of CompanySettings) Implements IBaseRepository(Of CompanySettings).Query
            Return GetAll().AsQueryable()
        End Function

        Public Function ExecuteStoredProcedure(Of T)(sp_name As String, parameters As IEnumerable(Of (String, Object))) As IEnumerable(Of T) Implements IBaseRepository(Of CompanySettings).ExecuteStoredProcedure
            Return Enumerable.Empty(Of T)()
        End Function

        Public Function ExecuteNonQuery(command As String, ParamArray params() As Object) As Integer Implements IBaseRepository(Of CompanySettings).ExecuteNonQuery
            Return 0
        End Function

        Public Function ExecuteQuery(Of T)(sqlQuery As String, ParamArray parameters() As Object) As IEnumerable(Of T) Implements IBaseRepository(Of CompanySettings).ExecuteQuery
            Return Enumerable.Empty(Of T)()
        End Function

        Public Function ExecuteQueryDt(sqlQuery As String, tablename As String, parameters As IEnumerable(Of (String, Object))) As DataTable Implements IBaseRepository(Of CompanySettings).ExecuteQueryDt
            Return New DataTable()
        End Function

        Public Function ExecuteQueryDR(Of T)(query As String, parameters As IEnumerable(Of (String, Object))) As IEnumerable(Of T) Implements IBaseRepository(Of CompanySettings).ExecuteQueryDR
            Return Enumerable.Empty(Of T)()
        End Function
    End Class

#End Region

End Class
