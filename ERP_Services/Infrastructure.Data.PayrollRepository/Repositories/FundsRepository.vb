Imports Domain.Payroll.Entities
Imports Infrastructure.Data.Base
Imports Domain.Payroll

Public Class FundsRepository
    Inherits GenericRepository(Of Fund)
    Implements IFundsLevelRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IPayrollUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Obtiene un Fondo
    ''' </summary>
    ''' <param name="code">Código del Fondo</param>
    ''' <returns>Fondo</returns>
    Public Function GetFunds(code As String, Optional tracking As Boolean = True) As Object Implements IFundsLevelRepository.GetFunds
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res As New Fund()
        If tracking Then
            res = (From d As Fund In Me._context.Fund
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
            If res IsNot Nothing Then
                Dim ThirdParty = (From c As ThirdParty In Me._context.ThirdParty.AsNoTracking() Where c.Id = res.ThirdPartyId Select c).FirstOrDefault
                res.NameThirdPartyFund = ThirdParty.Nit + " - " + ThirdParty.Name

                Dim Customer = (From c As Customer In Me._context.Customer.AsNoTracking() Where c.ThirdPartyId = res.ThirdPartyId Select c).FirstOrDefault
                If res.Term Is Nothing Then
                    If Customer IsNot Nothing Then
                        res.Term = Customer.Term
                    End If
                End If

                If res.MainAccountReceivableId Is Nothing Then
                    If Customer IsNot Nothing Then
                        res.MainAccountReceivableId = Customer.MainAccountReceivableId
                    End If
                End If

                If res.MainAccountReceivableId IsNot Nothing Then
                    Dim ObjAccount = (From c As MainAccounts In Me._context.MainAccounts.AsNoTracking() Where c.Id = res.MainAccountReceivableId Select c).FirstOrDefault
                    res.NameAccountReceivable = ObjAccount.Number + " - " + ObjAccount.Name
                End If
            End If
        Else
            res = (From g In _context.Fund.AsNoTracking
                                  Where g.Code.Equals(code.Trim())
                                  Select g).FirstOrDefault
            If res IsNot Nothing Then
                Dim ThirdParty = (From c As ThirdParty In Me._context.ThirdParty.AsNoTracking() Where c.Id = res.ThirdPartyId Select c).FirstOrDefault
                res.NameThirdPartyFund = ThirdParty.Nit + " - " + ThirdParty.Name

                Dim Customer = (From c As Customer In Me._context.Customer.AsNoTracking() Where c.ThirdPartyId = res.ThirdPartyId Select c).FirstOrDefault
                If res.Term Is Nothing Then
                    If Customer IsNot Nothing Then
                        res.Term = Customer.Term
                    End If
                End If

                If res.MainAccountReceivableId Is Nothing Then
                    If Customer IsNot Nothing Then
                        res.MainAccountReceivableId = Customer.MainAccountReceivableId
                    End If
                End If

                If res.MainAccountReceivableId IsNot Nothing Then
                    Dim ObjAccount = (From c As MainAccounts In Me._context.MainAccounts.AsNoTracking() Where c.Id = res.MainAccountReceivableId Select c).FirstOrDefault
                    res.NameAccountReceivable = ObjAccount.Number + " - " + ObjAccount.Name
                End If
            End If
        End If
        Return res
    End Function

    ''' <summary>
    ''' Lista Todos los Fondos
    ''' </summary>
    ''' <returns>Lista de Fondos</returns>
    Public Function ListAllFunds() As List(Of Fund) Implements IFundsLevelRepository.ListAllFunds
        Dim Funds = From e In _context.Fund
                                      Select e
        Return Funds.ToList()

    End Function

    ''' <summary>
    ''' Obtiene un tercero atraves del nit
    ''' </summary>
    ''' <param name="nit">Nit del tercero</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetThirdPartyByNit(nit As String) As Object Implements IFundsLevelRepository.GetThirdPartyByNit
        Dim thirdParties = From e In _context.ThirdParty.AsNoTracking().Include("Person").Include("Person.phone").Include("Person.Address").Include("Person.Email")
                         Where e.Nit = nit
                         Select e
        If thirdParties.Count > 0 Then
            Dim thirdparty = thirdParties.SingleOrDefault
            If thirdparty.Person IsNot Nothing
                If thirdparty.Person.Address IsNot Nothing AndAlso  thirdparty.Person.Address.Count > 0 Then
                    For Each address In thirdparty.Person.Address
                        If address.DepartmentId IsNot Nothing Then
                            address.DepartmentName = (From d In _context.Department Where d.Id = address.DepartmentId Select d.Name).FirstOrDefault()
                            If address.CityId IsNot Nothing Then
                                address.CityName = (From d In _context.City Where d.Id = address.CityId Select d.Name).FirstOrDefault()
                            End If
                        End If
                    Next
                End If
            End If
            Return thirdparty
        Else
            Return New ThirdParty()
        End If
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un fondo y todos sus agregados
    ''' </summary>
    ''' <param name="fund">Fondo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveFund(fund As Fund) As Boolean Implements IFundsLevelRepository.SaveFund
        _context.Fund.ApplyChanges(fund)
        Return True
    End Function

    ''' <summary>
    ''' Función que carga los fondos por Id
    ''' </summary>
    ''' <param name="IdFund"></param>
    ''' <returns>Fund</returns>
    ''' <remarks></remarks>
    Public Function GetFundsById(IdFund As Integer) As Fund Implements IFundsLevelRepository.GetFundsById

        Dim Funds = From e In _context.Fund.Include("ThirdParty")
        Where e.Id = IdFund
        Select e

        If Funds IsNot Nothing Then
            Return Funds.FirstOrDefault()
        Else
            Return New Fund
        End If
    End Function

    

End Class
