'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepositiry
' Author           : Andres Alarcon
' Created          : 29-11-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class StorageTemperatureRepository
    Inherits GenericRepository(Of StorageTemperature)
    Implements IStorageTemperatureRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Obtiene un rango de temperatura por codigo
    ''' </summary>
    Public Function GetStorageTemperature(code As String) As StorageTemperature Implements IStorageTemperatureRepository.GetStorageTemperature

        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If

        Return (From st In _context.StorageTemperature
                Where st.Code.Equals(code)
                Select st).FirstOrDefault()

    End Function

    ''' <summary>
    ''' Obtiene un rango de temperatura por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetInventoryProductById(id As Integer, Optional tracking As Boolean = True) As StorageTemperature Implements IStorageTemperatureRepository.GetStorageTemperatureById

        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If

        Return (From st In _context.StorageTemperature
                Where st.Id.Equals(id)
                Select st).FirstOrDefault()
    End Function

#End Region
End Class