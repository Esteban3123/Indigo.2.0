'***********************************************************************
' Assembly         : Application.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/09/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Transactions
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Resources
Imports System.Text

#End Region

Public Class CopyBaseAdminService
    Implements ICopyBaseAdminService

#Region "Variables"

    ''' <summary>
    ''' Repositorio para recursos o fuentes de financiación
    ''' </summary>
    ''' <remarks></remarks>
    Private _financialSourceRepository As IFinancialSourceRepository

    ''' <summary>
    ''' Repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _dependencyRepository As IBudgetDependencyRepository

    ''' <summary>
    ''' Repositorio para conceptos
    ''' </summary>
    ''' <remarks></remarks>
    Private _conceptRepository As IBudgetConceptRepository

    ''' <summary>
    ''' Repositorio para tipos de ingreso=1 y de gastos=2
    ''' </summary>
    ''' <remarks></remarks>
    Private _revenueTypeRepository As IEarningsTypeRepository

    ''' <summary>
    ''' Repositorio para rubros de ingreso=1 y rubros de gasto=2
    ''' </summary>
    ''' <remarks></remarks>
    Private _categoryRepository As IBudgetItemRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(financialSourceRepository As IFinancialSourceRepository, dependencyRepository As IBudgetDependencyRepository, conceptRepository As IBudgetConceptRepository,
                   revenueTypeRepository As IEarningsTypeRepository, categoryRepository As IBudgetItemRepository)
        If financialSourceRepository Is Nothing Then
            Throw New ArgumentNullException("financialSourceRepository")
        End If
        If dependencyRepository Is Nothing Then
            Throw New ArgumentNullException("dependencyRepository")
        End If
        If conceptRepository Is Nothing Then
            Throw New ArgumentNullException("conceptRepository")
        End If
        If revenueTypeRepository Is Nothing Then
            Throw New ArgumentNullException("revenueTypeRepository")
        End If
        If categoryRepository Is Nothing Then
            Throw New ArgumentNullException("categoryRepository")
        End If
        _financialSourceRepository = financialSourceRepository
        _dependencyRepository = dependencyRepository
        _conceptRepository = conceptRepository
        _revenueTypeRepository = revenueTypeRepository
        _categoryRepository = categoryRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Guarda la copia del presupuesto
    ''' </summary>
    ''' <param name="CopyBase"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveCopyBase(CopyBase As CopyBase, audit As AuditMessage) As ActionResult(Of CopyBase) Implements ICopyBaseAdminService.SaveCopyBase
        If CopyBase Is Nothing Then
            Throw New ArgumentNullException("CopyBase")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                'Listado de errores
                Dim ListErrors As New List(Of Tuple(Of String, Integer))

                'Se pregunta si se va a crear recursos o fuentes de financiacion
                If CopyBase.Resource Then
                    'Se consultan y se crean los recursos o fuentes de financiación
                    Dim result = GetAndSaveResource(CopyBase, audit)
                    If result.StateResult = False AndAlso result.StateResultAux = False Then 'Si hubo errores al guardar
                        Transaction.Dispose()
                        Return New ActionResult(Of CopyBase) With {.StateResult = False, .Message = result.Message}
                    End If
                    'Si no hubo errores al guardar se procede a capturar el listado de errores
                    ListErrors.AddRange(result.ObjectEmbbeded)
                End If

                'Se pregunta si se va a crear las dependencias
                If CopyBase.Dependency Then
                    'Se consultan y se crean las dependencias
                    Dim result = GetAndSaveDependency(CopyBase, audit)
                    If result.StateResult = False AndAlso result.StateResultAux = False Then 'Si hubo errores al guardar
                        Transaction.Dispose()
                        Return New ActionResult(Of CopyBase) With {.StateResult = False, .Message = result.Message}
                    End If
                    'Si no hubo errores al guardar se procede a capturar el listado de errores
                    ListErrors.AddRange(result.ObjectEmbbeded)
                End If

                'Se pregunta si se va a crear conceptos
                If CopyBase.Concepts Then
                    'Se consultan y se crean los conceptos
                    Dim result = GetAndSaveConcept(CopyBase, audit)
                    If result.StateResult = False AndAlso result.StateResultAux = False Then 'Si hubo errores al guardar
                        Transaction.Dispose()
                        Return New ActionResult(Of CopyBase) With {.StateResult = False, .Message = result.Message}
                    End If
                    'Si no hubo errores al guardar se procede a capturar el listado de errores
                    ListErrors.AddRange(result.ObjectEmbbeded)
                End If

                'Se pregunta si se va a crear tipos de ingreso
                If CopyBase.IncomeType Then
                    'Se consultan y se crean los tipos de ingreso
                    Dim result = GetAndSaveRevenueType(CopyBase, 1, audit)
                    If result.StateResult = False AndAlso result.StateResultAux = False Then 'Si hubo errores al guardar
                        Transaction.Dispose()
                        Return New ActionResult(Of CopyBase) With {.StateResult = False, .Message = result.Message}
                    End If
                    'Si no hubo errores al guardar se procede a capturar el listado de errores
                    ListErrors.AddRange(result.ObjectEmbbeded)
                End If

                'Se pregunta si se va a crear tipos de gasto
                If CopyBase.ExpenseType Then
                    'Se consultan y se crean los tipos de gasto
                    Dim result = GetAndSaveRevenueType(CopyBase, 2, audit)
                    If result.StateResult = False AndAlso result.StateResultAux = False Then 'Si hubo errores al guardar
                        Transaction.Dispose()
                        Return New ActionResult(Of CopyBase) With {.StateResult = False, .Message = result.Message}
                    End If
                    'Si no hubo errores al guardar se procede a capturar el listado de errores
                    ListErrors.AddRange(result.ObjectEmbbeded)
                End If

                'Se pregunta si se va a crear rubros de ingreso
                If CopyBase.CategoryIncome Then
                    'Se consultan y se crean los rubros de ingreso
                    Dim result = GetAndSaveCategory(CopyBase, 1, audit)
                    If result.StateResult = False AndAlso result.StateResultAux = False Then 'Si hubo errores al guardar
                        Transaction.Dispose()
                        Return New ActionResult(Of CopyBase) With {.StateResult = False, .Message = result.Message}
                    End If
                    'Si no hubo errores al guardar se procede a capturar el listado de errores
                    ListErrors.AddRange(result.ObjectEmbbeded)
                End If

                'Se pregunta si se va a crear rubros de gasto
                If CopyBase.CategoryExpense Then
                    'Se consultan y se crean los rubros de gasto
                    Dim result = GetAndSaveCategory(CopyBase, 2, audit)
                    If result.StateResult = False AndAlso result.StateResultAux = False Then 'Si hubo errores al guardar
                        Transaction.Dispose()
                        Return New ActionResult(Of CopyBase) With {.StateResult = False, .Message = result.Message}
                    End If
                    'Si no hubo errores al guardar se procede a capturar el listado de errores
                    ListErrors.AddRange(result.ObjectEmbbeded)
                End If

                'Se pregunta si se van a actualizar las dependencias y/o los rubros
                If CopyBase.UpdateParameterizedDependencies OrElse CopyBase.UpdateParameterizedCategories Then
                    Dim xmlCriterias = Me.ConvertCopyBaseToXmlCriterias(CopyBase, audit)
                    Dim result = _categoryRepository.SP_UpdateParameterizedInformation(xmlCriterias)
                    If result.Any(Function(d) d.Code <> 0) Then
                        For Each detail In result.Where(Function(d) d.Code <> 0)
                            ListErrors.Add(New Tuple(Of String, Integer)(detail.Message, 2))
                        Next
                    End If
                End If

                'Se asigna el listado de errores a la entidad que se retorna
                Transaction.Complete()
                CopyBase.ListErrors = ListErrors
                Return New ActionResult(Of CopyBase) With {.StateResult = True, .ObjectEmbbeded = CopyBase}
            Catch ex As Exception
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of CopyBase) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Metodo que consulta los recursos o fuentes de financiación de la vigencia de origen
    ''' y los guarda en la vigencia destino
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetAndSaveResource(CopyBase As CopyBase, audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Dim financialSourceUnitOfWork = _financialSourceRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try

                'Se obtienen todos los recursos o fuentes de financiación por la vigencia origen
                Dim ListFinancialSource = _financialSourceRepository.GetListFinancialSourceForCopyBase(CopyBase.ValidityIdSource)

                'Listado de errores
                Dim ListErrors As New List(Of Tuple(Of String, Integer))

                If ListFinancialSource Is Nothing OrElse ListFinancialSource.Count = 0 Then 'Si no existen recursos retorna el error
                    ListErrors.Add(New Tuple(Of String, Integer)(ResourceManager.GetString("DontExistFinancialSourceWithValiditySelected", "Budget"), 2))
                    Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .StateResultAux = True, .ObjectEmbbeded = ListErrors}
                End If

                'Se recorre los recursos encontrados
                For Each item In ListFinancialSource
                    'Se valida que el codigo no exista con la vigencia destino
                    Dim financialSource = _financialSourceRepository.GetFinancialSourceByCodeAndValidityForCopyBase(item.Code, CopyBase.ValidityIdDestiny)

                    If financialSource IsNot Nothing Then 'Si ya existe se coloca en el listado de errores y no se puede guardar este item
                        ListErrors.Add(New Tuple(Of String, Integer)(String.Format(ResourceManager.GetString("ExistFinancialSourceWithCode", "Budget"), financialSource.Code + " - " + financialSource.Name), 2))
                        Continue For
                    End If

                    'Si no existe se procede a crear el objeto para enviarlo a guardar
                    financialSource = New FinancialSource
                    With financialSource
                        .BudgetaryValidityId = CopyBase.ValidityIdDestiny
                        .Code = item.Code
                        .Name = item.Name
                        .Classification = item.Classification
                        .Status = item.Status
                        .CreationUser = audit.CodeUser
                        .CreationDate = DateTime.Now
                    End With

                    'Se procede a guardar la fuente de financiacion creada
                    _financialSourceRepository.SaveEntity(financialSource)
                    financialSourceUnitOfWork.Commit()
                Next

                Transaction.Complete()
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = True, .ObjectEmbbeded = ListErrors}
            Catch ex As Exception
                financialSourceUnitOfWork.Dispose()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .StateResultAux = False, .Message = ex.Message}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Metodo que consulta dependencias de la vigencia de origen
    ''' y los guarda en la vigencia destino
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetAndSaveDependency(CopyBase As CopyBase, audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Dim dependencyUnitOfWork = _dependencyRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try

                'Se obtienen todas las dependencias por la vigencia origen
                Dim ListDependency = _dependencyRepository.GetListDependencyForCopyBase(CopyBase.ValidityIdSource)

                'Listado de errores
                Dim ListErrors As New List(Of Tuple(Of String, Integer))

                If ListDependency Is Nothing OrElse ListDependency.Count = 0 Then 'Si no existen dependencias retorna el error
                    ListErrors.Add(New Tuple(Of String, Integer)(ResourceManager.GetString("DontExistDependencyWithValiditySelected", "Budget"), 2))
                    Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .StateResultAux = True, .ObjectEmbbeded = ListErrors}
                End If

                'Se recorre los recursos encontrados
                For Each item In ListDependency
                    'Se valida que el codigo no exista con la vigencia destino
                    Dim dependency = _dependencyRepository.GetDependencyByCodeAndValidityForCopyBase(item.Code, CopyBase.ValidityIdDestiny)

                    If dependency IsNot Nothing Then 'Si ya existe se coloca en el listado de errores y no se puede guardar este item
                        ListErrors.Add(New Tuple(Of String, Integer)(String.Format(ResourceManager.GetString("ExistDependencyWithCode", "Budget"), dependency.Code + " - " + dependency.Name), 2))
                        Continue For
                    End If

                    'Si no existe se procede a crear el objeto para enviarlo a guardar
                    dependency = New Dependency
                    With dependency
                        .BudgetaryValidityId = CopyBase.ValidityIdDestiny
                        .Code = item.Code
                        .Name = item.Name
                        .ResponsibleId = item.ResponsibleId
                        .Status = item.Status
                        .CreationUser = audit.CodeUser
                        .CreationDate = DateTime.Now
                    End With

                    'Se procede a guardar la dependencia creada
                    _dependencyRepository.SaveEntity(dependency)
                    dependencyUnitOfWork.Commit()
                Next

                Transaction.Complete()
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = True, .ObjectEmbbeded = ListErrors}
            Catch ex As Exception
                dependencyUnitOfWork.Dispose()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .StateResultAux = False, .Message = ex.Message}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Metodo que consulta dependencias de la vigencia de origen
    ''' y los guarda en la vigencia destino
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetAndSaveConcept(CopyBase As CopyBase, audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Dim conceptUnitOfWork = _conceptRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try

                'Se obtienen todos los conceptos por la vigencia origen
                Dim ListConcept = _conceptRepository.GetListConceptForCopyBase(CopyBase.ValidityIdSource)

                'Listado de errores
                Dim ListErrors As New List(Of Tuple(Of String, Integer))

                If ListConcept Is Nothing OrElse ListConcept.Count = 0 Then 'Si no existen conceptos retorna el error
                    Transaction.Complete()
                    ListErrors.Add(New Tuple(Of String, Integer)(ResourceManager.GetString("DontExistConceptWithValiditySelected", "Budget"), 2))
                    Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .StateResultAux = True, .ObjectEmbbeded = ListErrors}
                End If

                'Se recorre los recursos encontrados
                For Each item In ListConcept
                    'Se valida que el codigo no exista con la vigencia destino
                    Dim concept = _conceptRepository.GetConceptByCodeAndValidityForCopyBase(item.Code, CopyBase.ValidityIdDestiny)

                    If concept IsNot Nothing Then 'Si ya existe se coloca en el listado de errores y no se puede guardar este item
                        ListErrors.Add(New Tuple(Of String, Integer)(String.Format(ResourceManager.GetString("ExistConceptWithCode", "Budget"), concept.Code + " - " + concept.Name), 2))
                        Continue For
                    End If

                    'Si no existe se procede a crear el objeto para enviarlo a guardar
                    concept = New Concept
                    With concept
                        .BudgetaryValidityId = CopyBase.ValidityIdDestiny
                        .Code = item.Code
                        .Name = item.Name
                        .Status = item.Status
                        .CreationUser = audit.CodeUser
                        .CreationDate = DateTime.Now
                    End With

                    'Se procede a guardar el concepto creada
                    _conceptRepository.SaveEntity(concept)
                    conceptUnitOfWork.Commit()
                Next

                Transaction.Complete()
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = True, .ObjectEmbbeded = ListErrors}
            Catch ex As Exception
                conceptUnitOfWork.Dispose()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .StateResultAux = False, .Message = ex.Message}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Metodo que consulta tipos de ingreso=1 o tipos de gasto=2 de la vigencia de origen
    ''' y los guarda en la vigencia destino
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetAndSaveRevenueType(CopyBase As CopyBase, type As Integer, audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Dim revenueTypeUnitOfWork = _revenueTypeRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try

                'Se obtienen todos los tipos de ingreso=1 o tipos de gasto=2 por la vigencia origen
                Dim ListRevenueType = _revenueTypeRepository.GetListRevenueTypeForCopyBase(CopyBase.ValidityIdSource, type)

                'Listado de errores
                Dim ListErrors As New List(Of Tuple(Of String, Integer))

                If ListRevenueType Is Nothing OrElse ListRevenueType.Count = 0 Then 'Si no existen conceptos retorna el error
                    Dim typeName As String
                    If type = 1 Then 'Ingreso
                        typeName = "tipos de ingreso"
                    Else 'Gasto
                        typeName = "tipos de gasto"
                    End If
                    ListErrors.Add(New Tuple(Of String, Integer)(String.Format(ResourceManager.GetString("DontExistRevenueTypeWithValiditySelected", "Budget"), typeName), 2))
                    Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .StateResultAux = True, .ObjectEmbbeded = ListErrors}
                End If

                'Se recorre los recursos encontrados
                For Each item In ListRevenueType
                    'Se valida que el codigo no exista con la vigencia destino
                    Dim revenueType = _revenueTypeRepository.GetRevenueTypeByCodeAndValidityForCopyBase(item.Code, CopyBase.ValidityIdDestiny, type)

                    If revenueType IsNot Nothing Then 'Si ya existe se coloca en el listado de errores y no se puede guardar este item
                        Dim typeName As String
                        If type = 1 Then 'Ingreso
                            typeName = "ingreso"
                        Else 'Gasto
                            typeName = "gasto"
                        End If
                        ListErrors.Add(New Tuple(Of String, Integer)(String.Format(ResourceManager.GetString("ExistRevenueTypeWithCode", "Budget"), typeName, revenueType.Code + " - " + revenueType.Name), 2))
                        Continue For
                    End If

                    'Si no existe se procede a crear el objeto para enviarlo a guardar
                    revenueType = New RevenueType
                    With revenueType
                        .BudgetaryValidityId = CopyBase.ValidityIdDestiny
                        .Code = item.Code
                        .Name = item.Name
                        .Type = type
                        If type = 1 Then 'Ingreso
                            .IncomeSource = item.IncomeSource
                            .ExpenditureDefinition = Nothing
                            .ExpenditureClassification = Nothing
                        Else 'Gasto
                            .IncomeSource = Nothing
                            .ExpenditureDefinition = item.ExpenditureDefinition
                            .ExpenditureClassification = item.ExpenditureClassification
                        End If
                        .Status = item.Status
                        .CreationUser = audit.CodeUser
                        .CreationDate = DateTime.Now
                    End With

                    'Se procede a guardar el concepto creada
                    _revenueTypeRepository.SaveEntity(revenueType)
                    revenueTypeUnitOfWork.Commit()
                Next

                Transaction.Complete()
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = True, .ObjectEmbbeded = ListErrors}
            Catch ex As Exception
                revenueTypeUnitOfWork.Dispose()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .StateResultAux = False, .Message = ex.Message}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Metodo que consulta rubros de ingreso=1 o rubros de gasto=2 de la vigencia de origen
    ''' y los guarda en la vigencia destino
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetAndSaveCategory(CopyBase As CopyBase, type As Integer, audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Dim categoryUnitOfWork = _categoryRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                'Se obtienen todos los rubros de ingreso o rubros de gasto por la vigencia origen
                Dim ListCategory = _categoryRepository.GetListCategoryForCopyBase(CopyBase.ValidityIdSource, type)
                'Se obtienen todos las fuentes de financiacion de la vigencia destino
                Dim ListFinancialSource = _financialSourceRepository.GetListFinancialSourceForCopyBase(CopyBase.ValidityIdDestiny)

                'Listado de errores
                Dim ListErrors As New List(Of Tuple(Of String, Integer))

                If ListCategory Is Nothing OrElse ListCategory.Count = 0 Then 'Si no existen rubros retorna el error
                    Dim typeName As String
                    If type = 1 Then 'Ingreso
                        typeName = "rubros de ingreso"
                    Else 'Gasto
                        typeName = "rubros de gasto"
                    End If
                    ListErrors.Add(New Tuple(Of String, Integer)(String.Format(ResourceManager.GetString("DontExistCategoryWithValiditySelected", "Budget"), typeName), 2))
                    Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .StateResultAux = True, .ObjectEmbbeded = ListErrors}
                End If

                If ListFinancialSource Is Nothing OrElse ListFinancialSource.Count = 0 Then 'Si no existen recursos retorna el error
                    ListErrors.Add(New Tuple(Of String, Integer)(ResourceManager.GetString("DontExistFinancialSourceWithValiditySelected", "Budget"), 2))
                    Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .StateResultAux = True, .ObjectEmbbeded = ListErrors}
                End If

                'Se recorre los rubros encontrados
                For Each item In ListCategory
                    Dim financialSourceCode As String = Nothing
                    If item.FinancialSource IsNot Nothing Then
                        financialSourceCode = item.FinancialSource.Code
                    End If

                    'Se valida que el codigo no exista con la vigencia destino
                    Dim category = _categoryRepository.GetCategoryByCodeAndValidityForCopyBase(CopyBase.ValidityIdDestiny, type, item.Code, financialSourceCode)

                    If category IsNot Nothing Then 'Si ya existe se coloca en el listado de errores y no se puede guardar este item
                        Dim typeName As String
                        If type = 1 Then 'Ingreso
                            typeName = "ingreso"
                        Else 'Gasto
                            typeName = "gasto"
                        End If
                        ListErrors.Add(New Tuple(Of String, Integer)(String.Format(ResourceManager.GetString("ExistCategoryWithCode", "Budget"), typeName, category.Code + " - " + category.Name), 2))
                        Continue For
                    End If

                    'Si no esta creada se procede al metodo recursivo
                    RecursiveGetCategory(item.Id, ListCategory, ListFinancialSource, CopyBase, type, audit)
                Next

                Transaction.Complete()
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = True, .ObjectEmbbeded = ListErrors}
            Catch ex As Exception
                categoryUnitOfWork.Dispose()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .StateResultAux = False, .Message = ex.Message}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Id">Rubro que se esta recorriendo</param>
    ''' <param name="ListCategory">Listado de rubros para hacer las busquedas</param>
    ''' <remarks></remarks>
    Private Sub RecursiveGetCategory(Id As Integer, ListCategory As List(Of Category), ListFinancialSource As List(Of FinancialSource), CopyBase As CopyBase, type As Integer, audit As AuditMessage, Optional ListActualyRegisterId As List(Of Integer) = Nothing)
        Dim categoryUnitOfWork = _categoryRepository.UnitWork
        Try
            'Obtengo el registro con el Id
            Dim categoryParent = ListCategory.FindAll(Function(item) item.Id = Id).FirstOrDefault
            'Si lo encontro en el listado
            If categoryParent IsNot Nothing Then
                Dim financialSourceCode As String = Nothing
                If categoryParent.FinancialSource IsNot Nothing Then
                    financialSourceCode = categoryParent.FinancialSource.Code
                End If

                'Pregunto si ya existe el registro
                Dim categoryExist = _categoryRepository.GetCategoryByCodeAndValidityForCopyBase(CopyBase.ValidityIdDestiny, type, categoryParent.Code, financialSourceCode)
                If categoryExist Is Nothing Then 'Si no existe procedo
                    'Pregunto si es hijo
                    If categoryParent.CategoryOwnerId IsNot Nothing Then
                        'Obtengo el papa y pregunto si ya esta creado
                        Dim catPa = ListCategory.FindAll(Function(item) item.Id = categoryParent.CategoryOwnerId).FirstOrDefault

                        financialSourceCode = Nothing
                        If catPa.FinancialSource IsNot Nothing Then
                            financialSourceCode = catPa.FinancialSource.Code
                        End If

                        'Pregunto si ya esta creado el papa
                        Dim catPaExist = _categoryRepository.GetCategoryByCodeAndValidityForCopyBase(CopyBase.ValidityIdDestiny, type, catPa.Code, financialSourceCode)
                        'Si no existe el papa
                        If catPaExist Is Nothing Then
                            'Instancio el listado donde se va a asignar los items que no son agregados
                            If ListActualyRegisterId Is Nothing Then
                                ListActualyRegisterId = New List(Of Integer)
                            End If
                            ListActualyRegisterId.Add(categoryParent.Id)
                            RecursiveGetCategory(catPa.Id, ListCategory, ListFinancialSource, CopyBase, type, audit, ListActualyRegisterId)
                        Else 'Si existe el papa y el listado de ids esta lleno procedo a eliminar el item que se esta recorriendo
                            If ListActualyRegisterId IsNot Nothing AndAlso ListActualyRegisterId.Count > 0 Then
                                ListActualyRegisterId.Remove(categoryParent.Id)
                            End If
                        End If
                    End If

                    'Se crea la nueva entidad
                    Dim categorySave As New Category
                    With categorySave
                        .BudgetaryValidityId = CopyBase.ValidityIdDestiny
                        .ItemType = type
                        .Code = categoryParent.Code
                        .AlternativeCode = categoryParent.AlternativeCode
                        .Name = categoryParent.Name

                        If categoryParent.CategoryOwnerId Is Nothing Then
                            .CategoryOwnerId = Nothing
                        Else
                            Dim catPa = ListCategory.FindAll(Function(item) item.Id = categoryParent.CategoryOwnerId).FirstOrDefault

                            financialSourceCode = Nothing
                            If catPa.FinancialSource IsNot Nothing Then
                                financialSourceCode = catPa.FinancialSource.Code
                            End If

                            Dim catPaExist = _categoryRepository.GetCategoryByCodeAndValidityForCopyBase(CopyBase.ValidityIdDestiny, type, catPa.Code, financialSourceCode)
                            .CategoryOwnerId = catPaExist.Id
                        End If

                        Dim financialSourceId As Integer? = Nothing
                        If categoryParent.FinancialSource IsNot Nothing Then
                            Dim financialSource = ListFinancialSource.FirstOrDefault(Function(d) d.Code = categoryParent.FinancialSource.Code)
                            If financialSource IsNot Nothing Then
                                financialSourceId = financialSource.Id
                            End If
                        End If

                        .Auxiliary = categoryParent.Auxiliary
                        .FinancialSourceId = financialSourceId
                        .PAC = categoryParent.PAC
                        .StatusPAC = categoryParent.StatusPAC
                        .Used = categoryParent.Used
                        .FutureValidity = categoryParent.FutureValidity
                        .InvertionProject = categoryParent.InvertionProject
                        .BalanceDeficit = categoryParent.BalanceDeficit
                        .IncomeCxP = categoryParent.IncomeCxP
                        .ReservePacStatus = categoryParent.ReservePacStatus
                        .StatusPacCxP = categoryParent.StatusPacCxP
                        .Status = categoryParent.Status
                        .FundSituation = categoryParent.FundSituation
                        .CreationUser = audit.CodeUser
                        .CreationDate = DateTime.Now
                    End With

                    'Se guarda la nueva entidad
                    _categoryRepository.SaveEntity(categorySave)
                    categoryUnitOfWork.Commit()

                    If ListActualyRegisterId IsNot Nothing AndAlso ListActualyRegisterId.Count > 0 Then
                        Dim cont = ListActualyRegisterId.Count
                        RecursiveGetCategory(ListActualyRegisterId(cont - 1), ListCategory, ListFinancialSource, CopyBase, type, audit, ListActualyRegisterId)
                    End If
                End If
            End If
        Catch ex As Exception
            categoryUnitOfWork.RollbackChanges()
            Throw ex
        End Try
    End Sub

#End Region

#Region "Private Methods"

    Private Function ConvertCopyBaseToXmlCriterias(CopyBase As CopyBase, audit As AuditMessage) As String
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        builder.Append(String.Format("<{0}>{1}</{0}>", "ValidityIdDestiny", CopyBase.ValidityIdDestiny))
        builder.Append(String.Format("<{0}>{1}</{0}>", "Dependencies", CopyBase.UpdateParameterizedDependencies))
        builder.Append(String.Format("<{0}>{1}</{0}>", "Categories", CopyBase.UpdateParameterizedCategories))
        builder.Append(String.Format("<{0}>{1}</{0}>", "UserCode", audit.CodeUser))

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
            _financialSourceRepository = Nothing
            _dependencyRepository = Nothing
            _conceptRepository = Nothing
            _revenueTypeRepository = Nothing
            _categoryRepository = Nothing
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
