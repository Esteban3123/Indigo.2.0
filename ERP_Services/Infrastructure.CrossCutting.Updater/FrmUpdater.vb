'***********************************************************************
' Assembly         : SA.IndigoCrystal.Infraestructura.Transversal.Actualizador.frmActualizador
' Author           : WalterSierra
' Created          : 28-03-2011
'
' Last Modified By : WalterSierra
' Last Modified On : 28-03-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports System.IO
Imports System.Collections.ObjectModel
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Runtime.InteropServices

''' <summary>
''' Funcional encargado de avisar al usuario el estado del proceso de copia de la nueva version
''' </summary>
Public Class FrmUpdater

#Region "Metodos y Funciones necesarias para el funcionamiento del funcional"

    Private Delegate Sub UpdateResultsDelegate(message As String)

    ''' <summary>
    ''' Función para actualizar el label de mensajes
    ''' </summary>
    ''' <param name="message">Mensaje</param>
    Private Sub UpdateResults(message As String)
        If Me.INDlblMensaje2.InvokeRequired Then
            Dim dl As New UpdateResultsDelegate(AddressOf UpdateResults)
            Me.INDlblMensaje2.Invoke(dl, New Object() {message})
        Else
            Me.INDlblMensaje2.Text = message.Trim()
        End If
    End Sub

    Private Delegate Sub UpdateProgressBarDelegate(ByVal values As Int32, ByVal style As System.Windows.Forms.ProgressBarStyle)

    ''' <summary>
    ''' Actualiza la barra de progreso
    ''' </summary>
    ''' <param name="value">Valor a poner en la barra</param>
    Private Sub UpdateProgressBar(ByVal value As Int32, Optional ByVal style As System.Windows.Forms.ProgressBarStyle = ProgressBarStyle.Blocks)
        If Me.INDProgressBar.InvokeRequired Then
            Dim dl As New UpdateProgressBarDelegate(AddressOf UpdateProgressBar)
            Me.INDProgressBar.Invoke(dl, New Object() {value, style})
        Else
            Me.INDProgressBar.Value = value
            Me.INDProgressBar.Style = style
        End If
    End Sub

    ''' <summary>
    ''' metodo con el cual se empieza a copiar los archivos de ruta origen a ruta destino
    ''' </summary>
    Function ComenzarCopiadeArchivos(worker As BackgroundWorker) As Boolean
        Dim directorioServidor As New DirectoryInfo(IndigoUpdater.PathUpdateServer)
        Dim subdirectoriosServidor As DirectoryInfo() = directorioServidor.GetDirectories()
        Dim totalArchivos As Integer 'total de archivos a copiar
        Dim archivosCopiados As Integer = 0 'toal de archivos copiados
        'cuento el numero de archivos a copiar
        totalArchivos = My.Computer.FileSystem.GetFiles(IndigoUpdater.PathUpdateServer).Count
        For Each subdir As DirectoryInfo In subdirectoriosServidor
            totalArchivos += subdir.GetFiles.Length
        Next
        If totalArchivos = 0 Then
            'no hay archivos en la ruta de actualizaciones
            Return False
        End If
        Dim archivo As String
        'Dim rutaDestino As String = Application.StartupPath & "\"
        Dim rutaDestino As String = Infrastructure.CrossCutting.Base.Utils.AppFolder()
        'copio los archivos
        For Each archivosaCopiar As String In Directory.GetFiles(IndigoUpdater.PathUpdateServer, "*.*", SearchOption.AllDirectories)
            'establezco el archivo a copiar
            archivo = archivosaCopiar.Substring(IndigoUpdater.PathUpdateServer.Length + 1, archivosaCopiar.Length - IndigoUpdater.PathUpdateServer.Length - 1)
            'copio todos menos el actualizador
            If archivo <> My.Application.Info.AssemblyName & ".exe" Then
                Me.UpdateResults("Copiando el archivo: " & archivo)
                My.Computer.FileSystem.CopyFile(archivosaCopiar, rutaDestino & archivo, True)
                archivosCopiados += 1
                worker.ReportProgress(CInt((archivosCopiados * 100) / totalArchivos)) 'reporto el porcentaje
            End If
        Next
        'copio los directorios
        For Each subdir As DirectoryInfo In subdirectoriosServidor
            Me.UpdateResults("Copiando subcarpeta: " & subdir.Name)
            My.Computer.FileSystem.CopyDirectory(subdir.FullName, rutaDestino & subdir.Name, True)
            archivosCopiados += subdir.GetFiles.Length
            worker.ReportProgress(CInt((archivosCopiados * 100) / totalArchivos)) 'reporto el porcentaje
        Next
        Return True
    End Function

#End Region

#Region "Eventos y metodos del Funcional"

    ''' <summary>
    ''' EVENTO: DoWork del BackgroundWorker
    ''' </summary>
    Private Sub INDBackWorkerArchivos_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles INDBackWorkerArchivos.DoWork
        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)
        e.Result = ComenzarCopiadeArchivos(worker)
    End Sub

    ''' <summary>
    ''' EVENTO: ProgressChanged del BackgroundWorker
    ''' </summary>
    Private Sub INDBackWorkerArchivos_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles INDBackWorkerArchivos.ProgressChanged
        'actualizo el porcentaje de la barra
        If e.ProgressPercentage > 99 Then
            INDProgressBar.Value = 99
        Else
            INDProgressBar.Value = e.ProgressPercentage
        End If
    End Sub

    ''' <summary>
    ''' EVENTO: RunWorkerCompleted del BackgroundWorker
    ''' </summary>
    Private Async Sub INDBackWorkerArchivos_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles INDBackWorkerArchivos.RunWorkerCompleted
        'avisar al usuario que todo ha ido bien
        INDProgressBar.Value = 100
        Me.UpdateResults("Completado! 100%")
        Threading.Thread.Sleep(3000)
        Me.UpdateResults("Iniciando la optimización...")
        Threading.Thread.Sleep(1500)
        Me.UpdateProgressBar(100, ProgressBarStyle.Marquee)
        Me.UpdateResults("Optimizando ensamblados...")
        If Await IndigoUpdater.OptimizeAssemblyAsync() Then
            Me.UpdateResults("Optimización exitosa!")
            Threading.Thread.Sleep(3000)
            MessageBox.Show("Actualizada correctamente a la versión: " & IndigoUpdater.AssemblyVersion, My.Application.Info.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Me.UpdateResults("NO se ha podido realizar la optimización de ensamblados")
            Threading.Thread.Sleep(3000)
            MessageBox.Show("Actualizada la versión: " & IndigoUpdater.AssemblyVersion, My.Application.Info.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        'cuando se termine la copia lanzar de nuevo la aplicacion
        IndigoUpdater.RunAssembly()
        'terminar el programa
        End
    End Sub

    ''' <summary>
    ''' EVENTO: Load del Funcional
    ''' </summary>
    Private Sub frmActualizador_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.UpdateResults("Preparando archivos a copiar...")
        'esperamos 5 segundos antes de iniciar el proceso
        Threading.Thread.Sleep(5000)
        Me.UpdateResults("Copiando...")
        'lanzo el proceso de copia en segundo plano
        INDBackWorkerArchivos.RunWorkerAsync()
    End Sub

    ''' <summary>
    ''' Cierra el actualizador
    ''' </summary>
    Private Sub FrmUpdater_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If MessageBox.Show("Esta seguro que desea cancelar la actualización?" & vbCrLf & "Tenga en cuenta que si lo hace, puede causar errores en la versión actualmente instalada.", "Indigo Updater", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
            End
        Else
            e.Cancel = True
        End If
    End Sub

#End Region

End Class