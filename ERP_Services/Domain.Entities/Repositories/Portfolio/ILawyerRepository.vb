'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/06/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base
Imports Domain.Entities
#End Region

Public Interface ILawyerRepository
    Inherits IRepository(Of Lawyer)

#Region "Methods"

    ''' <summary>
    ''' metodo para obtener un abogado
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    Function GetLawyerByCode(ByVal code As String) As Lawyer

    ''' <summary>
    ''' metodo para obtener un abogado por id
    ''' </summary>
    ''' <returns></returns>
    Function GetLawyerById(ByVal id As Integer) As Lawyer

    ''' <summary>
    ''' Obtiene el abogado por id del tercero
    ''' </summary>
    ''' <param name="thirdPartyId"></param>
    ''' <returns></returns>
    Function GetLawyerByThirdPartyId(ByVal thirdPartyId As Integer, ByVal registerId As Integer) As Lawyer

#End Region

End Interface
