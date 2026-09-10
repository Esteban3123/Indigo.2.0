'************************************************************
' Assembly         : Infrastructure.Data.GlosasRepository
' Author           : Oscar Ivan Sierra
' Created          : 08-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
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

    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
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
            Dim query = From e In _context.Supplier.Include("PromptPaymentDiscount").Include("SupplierBankAccount.Currency").Include("ThirdParty").Include("ThirdParty.Person").Include("ThirdParty.Person.Address").Include("ThirdParty.Person.Phone").Include("ThirdParty.Person.Email").AsNoTracking
                        Where e.Code = codeSupplier
                        Select e
            If query.Count > 0 Then
                Dim supplier = query.FirstOrDefault()

                If supplier.ThirdParty IsNot Nothing AndAlso supplier.ThirdParty.Person IsNot Nothing Then
                    If supplier.ThirdParty.Person.Address IsNot Nothing AndAlso supplier.ThirdParty.Person.Address.Count > 0 Then
                        For Each address In supplier.ThirdParty.Person.Address
                            If address.DepartmentId IsNot Nothing Then
                                address.DepartmentName = (From d In _context.Department Where d.Id = address.DepartmentId Select d.Name).FirstOrDefault()
                                If address.CityId IsNot Nothing Then
                                    address.CityName = (From d In _context.City Where d.Id = address.CityId Select d.Name).FirstOrDefault()
                                End If
                            End If
                        Next
                    End If
                End If

                If supplier.SupplierBankAccount IsNot Nothing AndAlso supplier.SupplierBankAccount.Count > 0 Then
                    For Each objSupplierBankAcount As SupplierBankAccount In supplier.SupplierBankAccount
                        Dim ObjBank = (From d In _context.Bank Where d.Id = objSupplierBankAcount.BankId Select d).FirstOrDefault()
                        If ObjBank IsNot Nothing Then
                            objSupplierBankAcount.CodeNameBank = ObjBank.Code + " - " + ObjBank.Name
                        End If

                        If String.IsNullOrEmpty(objSupplierBankAcount?.Currency?.Abbreviation) Then
                            objSupplierBankAcount.CurrencyAbbreviation = (From x In _context.CompanySettings.AsNoTracking().Include("Currency") Select x)?.FirstOrDefault?.Currency?.Abbreviation
                        Else
                            objSupplierBankAcount.CurrencyAbbreviation = objSupplierBankAcount?.Currency?.Abbreviation
                        End If
                    Next

                End If


                Return supplier
            Else
                Return New Supplier()
            End If
        Else
            Dim query = From e In _context.Supplier.AsNoTracking.Include("SupplierBankAccount").Include("ThirdParty").Include("ThirdParty.Person").Include("ThirdParty.Person.Address").Include("ThirdParty.Person.Phone").Include("ThirdParty.Person.Email").AsNoTracking
                        Where e.Code = codeSupplier
                        Select e
            If query.Count > 0 Then
                Dim supplier = query.FirstOrDefault()

                If supplier.ThirdParty IsNot Nothing AndAlso supplier.ThirdParty.Person IsNot Nothing Then
                    If supplier.ThirdParty.Person.Address IsNot Nothing AndAlso supplier.ThirdParty.Person.Address.Count > 0 Then
                        For Each address In supplier.ThirdParty.Person.Address
                            If address.DepartmentId IsNot Nothing Then
                                address.DepartmentName = (From d In _context.Department Where d.Id = address.DepartmentId Select d.Name).FirstOrDefault()
                                If address.CityId IsNot Nothing Then
                                    address.CityName = (From d In _context.City Where d.Id = address.CityId Select d.Name).FirstOrDefault()
                                End If
                            End If
                        Next
                    End If
                End If

                If supplier.SupplierBankAccount IsNot Nothing AndAlso supplier.SupplierBankAccount.Count > 0 Then
                    For Each objSupplierBankAcount As SupplierBankAccount In supplier.SupplierBankAccount
                        Dim ObjBank = (From d In _context.Bank Where d.Id = objSupplierBankAcount.BankId Select d).FirstOrDefault()
                        If ObjBank IsNot Nothing Then
                            objSupplierBankAcount.CodeNameBank = ObjBank.Code + " - " + ObjBank.Name
                        End If
                    Next

                End If

                Return supplier
            Else
                Return New Supplier()
            End If
        End If
    End Function

    Public Function ListAllSupplier() As List(Of Domain.Entities.Supplier) Implements ISupplierRepository.ListAllSupplier
        Dim Busqueda = (From e In _context.Supplier
                        Select e).ToList()
        Return Busqueda
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
        Dim res = (From d As Domain.Entities.Supplier In Me._context.Supplier Where d.IdThirdParty = Id Select d).ToList()
        If res IsNot Nothing Then
            'res(0).OriginalValue = (From d As Domain.Entities.Supplier In Me._contextGlobal.Supplier.Include("ThirdParty").Include("AccountPayable").AsNoTracking() Where d.IdThirdParty = Id Select d).SingleOrDefault()
            Return res.FirstOrDefault()
        Else
            Return New Domain.Entities.Supplier()
        End If
    End Function

    ''' <summary>
    ''' obtiene un proveedor por id del tercero
    ''' </summary>
    ''' <param name="thirdPartyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSupplierByThirdPartyIdSimple(thirdPartyId As Integer) As Domain.Entities.Supplier Implements ISupplierRepository.GetSupplierByThirdPartyIdSimple
        Return (From s In _context.Supplier.AsNoTracking().Include("ThirdParty").AsNoTracking() Where s.IdThirdParty = thirdPartyId Select s).FirstOrDefault()
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
        'Dim res = (From d As Domain.Entities.Supplier In Me._context.Supplier.Include("ThirdParty").Include("AccountPayable").Include("AccountPayable.AccountPayableDetailConcept").Include("AccountPayable.AccountPayableDetailConcept.PaymentsConcept") Where d.IdThirdParty = IdThird And d.IdAccount = IdAccountAccounting Select d).ToList()
        'If res IsNot Nothing Then
        '    'res(0).OriginalValue = (From d As Domain.Entities.Supplier In Me._contextGlobal.Supplier.Include("ThirdParty").Include("AccountPayable").AsNoTracking() Where d.IdThirdParty = Id Select d).SingleOrDefault()
        '    Return res.FirstOrDefault()
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
        Dim res = (From d In Me._context.Supplier.Include("ThirdParty").AsNoTracking() Where d.Id = Id Select d)
        If res.Count > 0 Then
            Return res.SingleOrDefault
        Else
            Return New Supplier()
        End If
    End Function

    Public Function GetThirdPartyByIdSupplier(Id As Integer, Optional tracking As Boolean = True) As ThirdParty Implements ISupplierRepository.GetThirdPartyByIdSupplier
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.Supplier.AsNoTracking()
                   Join t In _context.ThirdParty.Include("AccountPayableConcepts").AsNoTracking() On d.IdThirdParty Equals t.Id
                   Where d.Id = Id Select t)
        If res.Count > 0 Then
            Return res.SingleOrDefault
        Else
            Return New ThirdParty()
        End If
    End Function

    Public Function GetIVARetentionPercentageBySupplierId(SupplierId As Integer) As Decimal Implements ISupplierRepository.GetIVARetentionPercentageBySupplierId
        If SupplierId = 0 Then
            Throw New ArgumentNullException("SupplierId")
        End If
        Return (From s In Me._context.Supplier.Include("ThirdParty").AsNoTracking()
                Join ac In _context.AccountPayableConcepts.AsNoTracking.Include("RetentionConcepts").AsNoTracking On ac.Id Equals s.ThirdParty.IVARetentionAccountPayableConceptId
                Where s.Id = SupplierId
                Select If(ac.RetentionConcepts IsNot Nothing, ac.RetentionConcepts.Rate, 0)).FirstOrDefault()
    End Function

    Public Function GetIVARetentionAccountPayableConceptBySupplierId(SupplierId As Integer) As AccountPayableConcepts Implements ISupplierRepository.GetIVARetentionAccountPayableConceptBySupplierId
        If SupplierId = 0 Then
            Throw New ArgumentNullException("SupplierId")
        End If
        Dim accountPayableConceptId = (From s In Me._context.Supplier.Include("ThirdParty").AsNoTracking()
                                       Where s.Id = SupplierId
                                       Select s.ThirdParty.IVARetentionAccountPayableConceptId).FirstOrDefault()

        If accountPayableConceptId IsNot Nothing Then
            Return Me._context.AccountPayableConcepts.Include("MainAccounts").
                                                      AsNoTracking().
                                                      Where(Function(ac) ac.Id = accountPayableConceptId).
                                                      FirstOrDefault()
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' obtiene un proveedor con las cuentas asociadas
    ''' </summary>
    ''' <param name="IdDistributionLines"></param>
    ''' <returns></returns>
    Public Function GetSupplierIdByIdDistributionLines(IdDistributionLines As Integer) As Integer Implements ISupplierRepository.GetSupplierIdByIdDistributionLines
        Return (From s In _context.SuppliersDistributionLines Where s.Id = IdDistributionLines Select s.IdSupplier).FirstOrDefault()
    End Function


    ''' <summary>
    ''' obtiene un proveedor con las cuentas asociadas
    ''' </summary>
    ''' <param name="supplierId"></param>
    ''' <returns></returns>
    Public Function GetSupplierByIdWithSupplierBankAccount(supplierId As Integer) As Supplier Implements ISupplierRepository.GetSupplierByIdWithSupplierBankAccount
        Dim res = (From s In _context.Supplier.Include("ThirdParty.Person.ADTIPOIDENTIFICA").Include("SupplierBankAccount") Where s.Id = supplierId Select s).FirstOrDefault()
        If res IsNot Nothing Then
            If res.ThirdParty?.Person IsNot Nothing Then
                Dim personId = res.ThirdParty.PersonId
                res.supplierEmail = (From e In _context.Email Where e.IdPerson = personId Select e.Email1)?.FirstOrDefault()
            End If
        Else
            Return New Supplier
        End If
        Return Res
    End Function

    Public Function ListSuppliersByIds(ids As List(Of Integer)) As List(Of Supplier) Implements ISupplierRepository.ListSuppliersByIds
        Return (From s As Supplier In _context.Supplier.Include("ThirdParty").Include("ThirdParty.Person") Where ids.Contains(s.Id) Select s).ToList()
    End Function

    Public Function GetSupplierByIdThirdPartyWithThirdAdded(Id As Integer) As Supplier Implements ISupplierRepository.GetSupplierByIdThirdPartyWithThirdAdded
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res = (From d As Domain.Entities.Supplier In Me._context.Supplier.Include("ThirdParty") Where d.IdThirdParty = Id Select d).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New Domain.Entities.Supplier()
        End If
    End Function

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
