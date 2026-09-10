'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base
Imports Domain.Entities
#End Region

Public Interface IAccountReceivableConceptRepository
    Inherits IRepository(Of AccountReceivableConcept)

#Region "Methods"
    ''' <summary>
    ''' metodo para obtener todos los conceptos de cuentas por pagar
    ''' </summary>
    ''' <returns></returns>
    Function GetAllAccountReceivableConcept()

    ''' <summary>
    ''' metodo para obtener un concepto de cuenta por pagar
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    Function GetAccountReceivableConceptByCode(ByVal code As String, Optional tracking As Boolean = True)

    ''' <summary>
    ''' metodo para obtener un concepto de cuenta por pagar por id
    ''' </summary>
    ''' <returns></returns>
    Function GetAccountReceivableConceptById(ByVal id As Integer) As AccountReceivableConcept

#End Region

End Interface
