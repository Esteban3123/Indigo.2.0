'************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Juan Carlos Polania Cortes
' Created          : 2023-03-14
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

Public Class ViewListDiagnosticImagingRepository
    Inherits GenericRepository(Of ViewListDiagnosticImaging)
    Implements IViewListDiagnosticImagingRepository

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
