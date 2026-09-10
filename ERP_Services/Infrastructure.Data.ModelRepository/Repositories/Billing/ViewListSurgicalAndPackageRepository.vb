'************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Diego A. Roldán
' Created          : 2022-05-13
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

Public Class ViewListSurgicalAndPackageRepository
    Inherits GenericRepository(Of ViewListSurgicalAndPackage)
    Implements IViewListSurgicalAndPackageRepository

    ' contexto del repositorio de ciudades
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''  Contructor del repositorio coidades el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

End Class
