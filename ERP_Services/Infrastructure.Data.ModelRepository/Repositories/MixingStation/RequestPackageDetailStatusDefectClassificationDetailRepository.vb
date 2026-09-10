'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Diego A. Roldan
' Created          : 2021-11-16
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class RequestPackageDetailStatusDefectClassificationDetailRepository
    Inherits GenericRepository(Of RequestPackageDetailStatusDefectClassificationDetail)
    Implements IRequestPackageDetailStatusDefectClassificationDetailRepository, Inject

    ''' <summary>
    ''' Contexto de Tipo de dosis unitaria
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de Tipo de Dosis Unitaria
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

End Class