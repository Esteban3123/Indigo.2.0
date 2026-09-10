'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Diego A. Roldan
' Created          : 2021-09-20
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class PackagePersonalizedRepository
    Inherits GenericRepository(Of PackagePersonalized)
    Implements IPackagePersonalizedRepository, Inject
    ''' <summary>
    ''' Contexto de PackageDetail
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de PackageDetail
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

End Class