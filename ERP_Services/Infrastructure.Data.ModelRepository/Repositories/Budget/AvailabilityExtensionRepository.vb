'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Juan Carlos Bermudez
' Created          : 23/09/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region

Public Class AvailabilityExtensionRepository
    Inherits GenericRepository(Of AvailabilityExtension)
    Implements IAvailabilityExtensionRepository

    'Contexto 
    Private _context As IGlobalModelUnitOfWork

#Region "Construct"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una prorroga de disponibilidad por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAvailabilityExtensionById(Id As Integer) As AvailabilityExtension Implements IAvailabilityExtensionRepository.GetAvailabilityExtensionById
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.AvailabilityExtension.AsNoTracking() Where d.Id = Id Select d).FirstOrDefault
        If res IsNot Nothing Then
            Return res
        Else
            Return New AvailabilityExtension
        End If
    End Function

#End Region

End Class
