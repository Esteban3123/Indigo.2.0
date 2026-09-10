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
Imports System.Threading.Tasks

#End Region

''' <summary>
''' Contrato de repositorio para la entidad tipo de documento
''' </summary>
Public Interface IDocumentTypeRepository
    Inherits IRepository(Of JournalVoucherTypes)

#Region "Methods"

    ''' <summary>
    ''' Obtiene un tipo de documento por su código
    ''' </summary>
    ''' <param name="code">Código del tipo de documento</param>
    ''' <returns>Tipo de documento</returns>
    Function GetDocumentTypeAsync(ByVal code As String) As Task(Of JournalVoucherTypes)

    ''' <summary>
    ''' Gets the journal voucher by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetJournalVoucherById(ByVal id As Long) As JournalVoucherTypes

#End Region

End Interface