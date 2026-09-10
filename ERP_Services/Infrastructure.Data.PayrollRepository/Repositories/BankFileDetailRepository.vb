Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Base
Public Class BankFileDetailRepository
    Inherits GenericRepository(Of BankFileDetail)
    Implements IBankFileDetailRepository

    #Region "Builder"

    ''' <summary>
    ''' Contexto
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Inicia el contexto
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' obtiene un archivo plano de bancos por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetBankFileDetailByBankFileId(id As Integer) As List(Of BankFileDetail) Implements IBankFileDetailRepository.GetBankFileDetailByBankFileId
        Dim res = (From bfd In _context.BankFileDetail Where bfd.BankFileId = id Select bfd).ToList()
        Return res
    End Function

#End Region

End Class
