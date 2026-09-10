'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Rafael Eduardo Patiño
' Created          : 03-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IKindsAgreementsRepository
    Inherits IRepository(Of KindsAgreements)

    ''' <summary>
    ''' Lista las clases de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListKindsAgreements() As List(Of KindsAgreements)

    ''' <summary>
    ''' Funcion para cargar una clase de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetKindsAgreements(ByVal code As String, Optional tracking As Boolean = True) As KindsAgreements


End Interface
