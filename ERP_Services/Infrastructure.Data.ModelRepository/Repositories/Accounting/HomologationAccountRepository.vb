'***********************************************************************
' Assembly         : Infrastructure.Data.AccountingRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/12/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region

Public Class HomologationAccountRepository
    Inherits GenericRepository(Of HomologationAccount)
    Implements IHomologationAccountRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

#Region "Builder"
    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub
#End Region

#Region "Functions"

    ''' <summary>
    ''' Guarda las homologaciones de cuenta
    ''' </summary>
    ''' <param name="xmlObject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveHomologationAccount(xmlObject As String) As SP_SaveHomologationAccount_Result Implements IHomologationAccountRepository.SaveHomologationAccount
        Return _context.SP_SaveHomologationAccount(xmlObject).SingleOrDefault
    End Function

#End Region

End Class
