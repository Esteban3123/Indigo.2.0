'************************************************************
' Assembly         : Infrastructure.Data.GlosasRepository
' Author           : Oscar Ivan Sierra
' Created          : 08-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Maintenance.Entities
Imports Domain.Maintenance
Imports Domain.Base.Entities
Imports Domain.Base
Imports Infrastructure.Data.ModelRepository

#End Region


''' <summary>
''' clase para hacer todas las operaciones de persistencia para la fabricante
''' </summary>
''' <remarks></remarks>
Public Class SupplierRepository
    Inherits GenericRepository(Of Supplier)
    Implements ISupplierRepository


    'Devuelve el contexto en este repositorio 
    Private _context As IMaintenanceModelUnitOfWork
    Private _contextGlobal As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IMaintenanceModelUnitOfWork, contextGlobal As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _contextGlobal = contextGlobal
        _context = contex
    End Sub

    ''' <summary>
    ''' Obtiene un proveedor por codigo
    ''' </summary>
    ''' <param name="codeSupplier"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSupplier(codeSupplier As String, Optional tracking As Boolean = True) As Supplier Implements ISupplierRepository.GetSupplier
        If codeSupplier Is Nothing OrElse codeSupplier.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        If tracking = True Then
            Dim query = From e In _context.Supplier.Include("SupplierBankAccount").Include("ThirdParty").Include("ThirdParty.Person").Include("ThirdParty.Person.Address").Include("ThirdParty.Person.Phone").Include("ThirdParty.Person.Email").AsNoTracking
                    Where e.Code = codeSupplier
                    Select e
            If query.Count > 0 Then
                Dim supplier = query.FirstOrDefault()
                For Each item In supplier.SupplierBankAccount
                    item.CodeNameBank = (From b In _contextGlobal.Bank.AsNoTracking() Where b.Id = item.BankId Select String.Concat(b.Code, " - ", b.Name)).FirstOrDefault()
                Next
                Return supplier
            Else
                Return New Supplier()
            End If
        Else
            Dim query = From e In _context.Supplier.AsNoTracking.Include("ThirdParty").Include("ThirdParty.Person").Include("ThirdParty.Person.Address").Include("ThirdParty.Person.Phone").Include("ThirdParty.Person.Email").AsNoTracking
                    Where e.Code = codeSupplier
                    Select e
            If query.Count > 0 Then
                Return query.FirstOrDefault()
            Else
                Return New Supplier()
            End If
        End If
    End Function

    Public Function ListAllSupplier() As List(Of Supplier) Implements ISupplierRepository.ListAllSupplier
        Dim Busqueda = From e In _context.Supplier
                       Select e


        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' Obtiene un proveedor por id del tercero
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetSupplierByIdThirdParty(Id As Integer) As Domain.Entities.Supplier Implements ISupplierRepository.GetSupplierByIdThirdParty
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res = (From d As Domain.Entities.Supplier In Me._contextGlobal.Supplier.Include("ThirdParty").Include("AccountPayable").Include("AccountPayable.AccountPayableDetailConcept").Include("AccountPayable.AccountPayableDetailConcept.AccountPayableConcepts") Where d.IdThirdParty = Id Select d).FirstOrDefault()
        If res IsNot Nothing Then
            'res(0).OriginalValue = (From d As Domain.Entities.Supplier In Me._contextGlobal.Supplier.Include("ThirdParty").Include("AccountPayable").AsNoTracking() Where d.IdThirdParty = Id Select d).SingleOrDefault()
            Return res
        Else
            Return New Domain.Entities.Supplier()
        End If
    End Function

    Public Function GetSupplierByIdThirdPartyWithThirdAdded(Id As Integer) As Domain.Entities.Supplier Implements ISupplierRepository.GetSupplierByIdThirdPartyWithThirdAdded
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res = (From d As Domain.Entities.Supplier In Me._contextGlobal.Supplier.Include("ThirdParty") Where d.IdThirdParty = Id Select d).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New Domain.Entities.Supplier()
        End If
    End Function


    ''' <summary>
    ''' Obtiene un proveedor por id del tercero e id de la cuenta contable
    ''' </summary>
    Public Function GetSupplierByIdThirdPartyAndIdAccountAccounting(IdThird As Integer, IdAccountAccounting As Integer) As Domain.Entities.Supplier Implements ISupplierRepository.GetSupplierByIdThirdPartyAndIdAccountAccounting
        If IdThird = 0 Then
            Throw New ArgumentNullException("IdThird")
        End If
        If IdAccountAccounting = 0 Then
            Throw New ArgumentNullException("IdAccountAccounting")
        End If
        'Dim res = (From d As Domain.Entities.Supplier In Me._contextGlobal.Supplier.Include("ThirdParty").Include("AccountPayable").Include("AccountPayable.AccountPayableDetailConcept").Include("AccountPayable.AccountPayableDetailConcept.PaymentsConcept") Where d.IdThirdParty = IdThird And d.IdAccount = IdAccountAccounting Select d).ToList()
        'If res IsNot Nothing Then
        '    'res(0).OriginalValue = (From d As Domain.Entities.Supplier In Me._contextGlobal.Supplier.Include("ThirdParty").Include("AccountPayable").AsNoTracking() Where d.IdThirdParty = Id Select d).SingleOrDefault()
        '    Return CType(res, Domain.Entities.Supplier)
        'Else
        Return New Domain.Entities.Supplier()
        'End If
    End Function

    Public Function SaveSupplier(Supplier As Supplier) As Boolean Implements ISupplierRepository.SaveSupplier
        _context.Supplier.ApplyChanges(Supplier)
        Return True
    End Function

    Public Function GetThirdPartyById(Id As Integer) As ThirdParty Implements ISupplierRepository.GetThirdPartyById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = From e In _context.ThirdParty.Include("Person").Include("Person.Address").Include("Person.Email").Include("Person.Phone").AsNoTracking
                    Where e.Id = Id
                    Select e

        If query.Count > 0 Then
            'res(0).OriginalValue = (From d As Supplier In Me._context.Supplier.Include("Person").AsNoTracking() Where d.Code.Equals(codeSupplier.Trim()) Select d).SingleOrDefault()
            Return query.FirstOrDefault()
        Else
            Return New ThirdParty()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un proveedor por su id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSupplierById(Id As Integer, Optional tracking As Boolean = True) As Supplier Implements ISupplierRepository.GetSupplierById
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.Supplier.AsNoTracking().Include("ThirdParty") Where d.Id = Id Select d).FirstOrDefault
        If res IsNot Nothing Then
            Return res
        Else
            Return New Supplier()
        End If
    End Function

    Public Function GetThirdPartyByIdSupplier(Id As Integer, Optional tracking As Boolean = True) As ThirdParty Implements ISupplierRepository.GetThirdPartyByIdSupplier
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.Supplier.AsNoTracking() _
                   Join t In _context.ThirdParty On d.IdThirdParty Equals t.Id
                   Where d.Id = Id Select t).ToList
        If res.Count > 0 Then
            Return res.SingleOrDefault
        Else
            Return New ThirdParty()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un proveedor por el nit del tercero
    ''' </summary>
    ''' <param name="nitThirdParty"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSupplierByNitThirdParty(nitThirdParty As String) As Supplier Implements ISupplierRepository.GetSupplierByNitThirdParty
        If nitThirdParty Is Nothing OrElse nitThirdParty.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("nitThirdParty")
        End If
        Dim query = (From s In _context.Supplier.Include("ThirdParty")
                    Where s.ThirdParty.Nit = nitThirdParty
                    Select s).FirstOrDefault
        If query IsNot Nothing Then
            Return query
        Else
            Return New Supplier()
        End If
    End Function

End Class
