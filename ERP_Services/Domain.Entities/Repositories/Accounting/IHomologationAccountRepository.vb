'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Juan F. Tamayo
' Created          : 2014-03-17
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Contrato de repositorio para la entidad clase contable
''' </summary>
Public Interface IHomologationAccountRepository
    Inherits IRepository(Of HomologationAccount)

    ''' <summary>
    ''' Crea las homologaciones de cuenta con un store procedure y enviando los objetos como Xml
    ''' </summary>
    ''' <param name="xmlObject">Objeto xml armado con el listado que se envia desde presentation</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveHomologationAccount(xmlObject As String) As SP_SaveHomologationAccount_Result

End Interface