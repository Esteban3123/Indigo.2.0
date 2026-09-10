'************************************************************
' Assembly         : Infraestructure.FisedAssets.BlockRepository
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 8-04-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************
#Region "Librerias importadas"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region
Public Class BlockRecordFixedAssetRepository
    Inherits GenericRepository(Of BlockRecordFixedAsset)
    Implements IBlockRecordFixedAssetRepository


    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

#Region "Constructor"
    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub



#End Region


    ''' <summary>
    ''' Gets the block record  by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetBlockRecordFixedAssetByIdformAndIdRecord(IdForm As String, IdRecord As String, Optional tracking As Boolean = True) As BlockRecordFixedAsset Implements IBlockRecordFixedAssetRepository.GetBlockRecordFixedAssetByIdformAndIdRecord
        If tracking Then
            Dim blockRecord = From e In _context.BlockRecordFixedAsset
                    Where e.IdForm = IdForm AndAlso e.IdRecord = IdRecord
                    Select e
            If blockRecord.Count > 0 Then
                Return blockRecord.FirstOrDefault
            Else
                Return New BlockRecordFixedAsset()
            End If
        Else
            Dim blockRecord = (From e In _context.BlockRecordFixedAsset.AsNoTracking
                                Where e.IdForm = IdForm AndAlso e.IdRecord = IdRecord
                                Select e).FirstOrDefault

            If blockRecord IsNot Nothing > 0 Then
                Return blockRecord
            Else
                Return New BlockRecordFixedAsset()
            End If
        End If
    End Function
End Class
