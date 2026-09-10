'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Juan Carlos Bermudez
' Created          : 29-07-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IPortfolioReclassificationAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' metodo para obtener una reclasificacion de documento por codigo
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    Function GetReclassificationByCode(ByVal code As String, ByVal audit As AuditMessage) As PortfolioReclassification

#End Region

End Interface
