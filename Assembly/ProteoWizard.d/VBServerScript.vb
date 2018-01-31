﻿Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.CommandLine
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Parallel.Threads
Imports SMRUCC.WebCloud.HTTPInternal
Imports SMRUCC.WebCloud.HTTPInternal.AppEngine
Imports SMRUCC.WebCloud.HTTPInternal.AppEngine.APIMethods
Imports SMRUCC.WebCloud.HTTPInternal.AppEngine.APIMethods.Arguments
Imports SMRUCC.WebCloud.HTTPInternal.Platform

''' <summary>
''' VB server script
''' </summary>
''' <remarks>
''' Usage: msconvert [options] [filemasks]
''' Convert mass spec data file formats.
'''
''' Return value :  # of failed files.
'''
''' Options:
'''  -f [ --filelist ] arg            : specify text file containing filenames
'''  -o [ --outdir ] arg (=.)         : Set output directory ('-' for stdout) [.]
'''  -c [ --config ] arg              : configuration file(optionName = value)
'''  --outfile arg                    : Override the name Of output file.
'''  -e [ --ext ] arg                 : Set extension For output files [mzML|mzXML|mgf|txt|mz5]
'''  --mzML                           : write mzML format [default]
'''  --mzXML                          : write mzXML format
'''  --mz5                            : write mz5 format
'''  --mgf                            : write Mascot generic format
'''  --text                           : write ProteoWizard internal text format
'''  --ms1                            : write MS1 format
'''  --cms1                           : write CMS1 format
'''  --ms2                            : write MS2 format
'''  --cms2                           : write CMS2 format
'''  -v [ --verbose ]                 : display detailed progress information
'''  --64                             : Set Default binary encoding To 64-bit precision [default]
'''  --32                             : Set Default binary encoding To 32-bit precision
'''  --mz64                           : encode m/z values In 64-bit precision [default]
'''  --mz32                           : encode m/z values In 32-bit precision
'''  --inten64                        : encode intensity values In 64-bit precision
'''  --inten32                        : encode intensity values In 32-bit precision [default]
'''  --noindex                        : Do Not write index
'''  -i [ --contactInfo ] arg         : filename for contact info
'''  -z [ --zlib ]                    : use zlib compression For binary data
'''  --numpressLinear [=arg(=2e-009)] : use numpress linear prediction compression For binary mz And rt data (relative accuracy loss will Not exceed given tolerance arg, unless Set To 0)
'''  --numpressPic                    : use numpress positive Integer compression For binary intensities (absolute accuracy loss will Not exceed 0.5)
'''  --numpressSlof [=arg(=0.0002)]   : use numpress Short logged float compression For binary intensities (relative accuracy loss will Not exceed given tolerance arg, unless Set To 0)
'''  -n [ --numpressAll ]             : same as --numpressLinear --numpressSlof (see https://github.com/fickludd/ms-numpress for more info)
'''  -g [ --gzip ]                    : gzip entire output file (adds .gz To filename)
'''  --filter arg                     : add a spectrum list filter
'''  --merge                          : create a Single output file from multiple input files by merging file-level metadata And concatenating spectrum lists
'''  --simAsSpectra                   : write selected ion monitoring As spectra, Not chromatograms
'''  --srmAsSpectra                   : write selected reaction monitoring As spectra, Not chromatograms
'''  --combineIonMobilitySpectra      : write all drift bins/scans In a frame/block As one spectrum instead Of individual spectra
'''  --acceptZeroLengthSpectra        : some vendor readers have an efficient way Of filtering out empty spectra, but it takes more time To open the file
'''  --ignoreUnknownInstrumentError   : If True, If an instrument cannot be determined from a vendor file, it will Not be an Error
'''  --help                           : show this message, With extra detail On filter options
'''
''' FILTER OPTIONS
''' run this command With --help To see more detail
''' index &lt;index_value_set>
''' msLevel &lt;mslevels>
''' chargeState &lt;charge_states>
''' precursorRecalculation
''' mzRefiner input1.pepXML input2.mzid [msLevels=&lt;1->] [thresholdScore=&lt;CV_Score_Name>] [thresholdValue=&lt;floatset>] [thresholdStep=&lt;float>] [maxSteps=&lt;count>]
''' lockmassRefiner mz=&lt;real> mzNegIons=&lt;real (mz)> tol=&lt;real (1.0 Daltons)>
''' precursorRefine
''' peakPicking [&lt;PickerType> [snr=&lt;minimum signal-to-noise ratio>] [peakSpace=&lt;minimum peak spacing>] [msLevel=&lt;ms_levels>]]
''' scanNumber &lt;scan_numbers>
''' scanEvent &lt;scan_event_set>
''' scanTime &lt;scan_time_range>
''' sortByScanTime
''' stripIT
''' metadataFixer
''' titleMaker &lt;format_string>
''' threshold &lt;type>&lt;threshold>&lt;orientation> [&lt;mslevels>]
''' mzWindow &lt;mzrange>
''' mzPrecursors &lt;precursor_mz_list> [mzTol=&lt;mzTol (10 ppm)>] [mode=&lt;include|exclude (include)>]
''' defaultArrayLength &lt;peak_count_range>
''' zeroSamples &lt;mode> [&lt;MS_levels>]
''' mzPresent &lt;mz_list> [mzTol=&lt;tolerance> (0.5 mz)] [type=&lt;type> (count)] [threshold=&lt;threshold> (10000)] [orientation=&lt;orientation> (most-intense)] [mode=&lt;include|exclude (include)>]
''' scanSumming [precursorTol=&lt;precursor tolerance>] [scanTimeTol=&lt;scan time tolerance>]
''' thermoScanFilter &lt;exact|contains>&lt;include|exclude>&lt;match string>
''' MS2Denoise [&lt;peaks_in_window> [&lt;window_width_Da> [multicharge_fragment_relaxation]]]
''' MS2Deisotope [hi_res [mzTol=&lt;mzTol>]] [Poisson [minCharge=&lt;minCharge>] [maxCharge=&lt;maxCharge>]]
''' ETDFilter [&lt;removePrecursor> [&lt;removeChargeReduced> [&lt;removeNeutralLoss> [&lt;blanketRemoval> [&lt;matchingTolerance> ]]]]]
''' demultiplex massError=&lt;tolerance and units, eg 0.5Da (default 10ppm)> nnlsMaxIter=&lt;int (50)> nnlsEps=&lt;real (1e-10)> noWeighting=&lt;bool (false)> demuxBlockExtra=&lt;real (0)> variableFill=&lt;bool (false)> noSumNormalize=&lt;bool (false)> optimization=&lt;(none)|overlap_only
''' chargeStatePredictor [overrideExistingCharge=&lt;true|false (false)>] [maxMultipleCharge=&lt;int (3)>] [minMultipleCharge=&lt;int (2)>] [singleChargeFractionTIC=&lt;real (0.9)>] [maxKnownCharge=&lt;int (0)>] [makeMS2=&lt;true|false (false)>]
''' turbocharger [minCharge=&lt;minCharge>] [maxCharge=&lt;maxCharge>] [precursorsBefore=&lt;before>] [precursorsAfter=&lt;after>] [halfIsoWidth=&lt;half-width of isolation window>] [defaultMinCharge=&lt;defaultMinCharge>] [defaultMaxCharge=&lt;defaultMaxCharge>] [useVendorPeaks=&lt;useVe
'''                                                                                                                                                                                                                                                                   activation &lt;precursor_activation_type>
''' analyzer &lt;analyzer>
''' analyzerType &lt;analyzer>
''' polarity &lt;polarity>
'''
'''
''' Examples:
'''
''' # convert data.RAW to data.mzML
''' msconvert data.RAW
'''
''' # convert data.RAW to data.mzXML
''' msconvert data.RAW --mzXML
'''
''' # put output file in my_output_dir
''' msconvert data.RAW -o my_output_dir
'''
''' # combining options to create a smaller mzML file, much like the old ReAdW converter program
''' msconvert data.RAW --32 --zlib --filter "peakPicking true 1-" --filter "zeroSamples removeExtra"
'''
''' # extract scan indices 5...10 and 20...25
''' msconvert data.RAW --filter "index [5,10] [20,25]"
'''
''' # extract MS1 scans only
''' msconvert data.RAW --filter "msLevel 1"
'''
''' # extract MS2 and MS3 scans only
''' msconvert data.RAW --filter "msLevel 2-3"
'''
''' # extract MSn scans for n>1
''' msconvert data.RAW --filter "msLevel 2-"
'''
''' # apply ETD precursor mass filter
''' msconvert data.RAW --filter ETDFilter
'''
''' # remove non-flanking zero value samples
''' msconvert data.RAW --filter "zeroSamples removeExtra"
'''
''' # remove non-flanking zero value samples in MS2 and MS3 only
''' msconvert data.RAW --filter "zeroSamples removeExtra 2 3"
'''
''' # add missing zero value samples (with 5 flanking zeros) in MS2 and MS3 only
''' msconvert data.RAW --filter "zeroSamples addMissing=5 2 3"
'''
''' # keep only HCD spectra from a decision tree data file
''' msconvert data.RAW --filter "activation HCD"
'''
''' # keep the top 42 peaks or samples (depending on whether spectra are centroid or profile):
''' msconvert data.RAW --filter "threshold count 42 most-intense"
'''
''' # multiple filters: select scan numbers and recalculate precursors
''' msconvert data.RAW --filter "scanNumber [500,1000]" --filter "precursorRecalculation"
'''
''' # multiple filters: apply peak picking and then keep the bottom 100 peaks:
''' msconvert data.RAW --filter "peakPicking true 1-" --filter "threshold count 100 least-intense"
'''
''' # multiple filters: apply peak picking and then keep all peaks that are at least 50% of the intensity of the base peak:
''' msconvert data.RAW --filter "peakPicking true 1-" --filter "threshold bpi-relative .5 most-intense"
'''
''' # use a configuration file
''' msconvert data.RAW -c config.txt
'''
''' # example configuration file
''' mzXML=true
''' zlib=true
''' filter="index [3,7]"
''' filter="precursorRecalculation"
'''
'''
''' Questions, comments, and bug reports:
''' http://proteowizard.sourceforge.net
''' support@proteowizard.org
'''
''' ProteoWizard release: 3.0.10650 (2017-3-27)
''' ProteoWizard MSData: 3.0.10577 (2017-3-6)
''' ProteoWizard Analysis: 3.0.10650 (2017-3-27)
''' Build date: Mar 28 2017 00:21:42
''' </remarks>
<[Namespace]("ProteoWizard.d")>
Public Class VBServerScript : Inherits WebApp

    ''' <summary>
    ''' + msconvert
    ''' 
    ''' ProteoWizard命令行程序的位置
    ''' </summary>
    ReadOnly BIN$
    ''' <summary>
    ''' OSS存储的文件系统位置
    ''' </summary>
    ReadOnly OSS_ROOT$

    Dim taskPool As New ThreadPool

    Sub New(main As PlatformEngine)
        Call MyBase.New(main)

        BIN = App.GetVariable("bin")
        OSS_ROOT = App.GetVariable("oss")

        Call $"msconvert={BIN}".__INFO_ECHO
        Call $"OSS_ROOT={OSS_ROOT}".__INFO_ECHO

        If Not OSS_ROOT.DirectoryExists Then
            Throw New Exception("OSS file system should be mounted at first!")
        End If
    End Sub

    <ExportAPI("/ProteoWizard.d/mzXML.vbs")>
    <Usage("/ProteoWizard.d/mzXML.vbs?path=<path>")>
    <[GET](GetType(String))>
    Public Function ConvertTomzXML(request As HttpRequest, response As HttpResponse) As Boolean
        Dim path$ = OSS_ROOT & "/" & request.URLParameters("path")
        Dim out$ = path.ParentPath & "/msconvert"
        Dim args$ = $"{path.CLIPath} --mz64 --mzXML --filter ""msLevel 1-2"" --ignoreUnknownInstrumentError -o {out.CLIPath}"

        Call New IORedirectFile(BIN, args).Run()

        If Not response Is Nothing Then
            Call response.SuccessMsg("Task complete!")
        End If

        Return True
    End Function

    <ExportAPI("/ProteoWizard.d/mzXML.task.vbs")>
    <Usage("/ProteoWizard.d/mzXML.task.vbs?path=<path>")>
    <[GET](GetType(String))>
    Public Function ConvertTomzXMLTask(request As HttpRequest, response As HttpResponse) As Boolean
        Dim task = Sub() ConvertTomzXML(request, Nothing)

        Call taskPool.RunTask(task)
        Call response.SuccessMsg("Task pending...")

        Return True
    End Function

    <ExportAPI("/ProteoWizard.d/MRM.vbs")>
    <Usage("/ProteoWizard.d/MRM.vbs?path=<path>")>
    <[GET](GetType(String))>
    Public Function MRMTask(request As HttpRequest, response As HttpResponse) As Boolean
        Dim path$ = OSS_ROOT & "/" & request.URLParameters("path")
        Dim out$ = request.URLParameters("to") Or $"{path.ParentPath}/msconvert".AsDefault

        If InStr(out, ":\") = 0 Then
            out = OSS_ROOT & "/" & out
        End If

        Dim args$ = $"{path.GetFullPath.CLIPath} --mz64 --mzML --filter ""msLevel 1-2"" --ignoreUnknownInstrumentError -o {out.GetDirectoryFullPath.CLIPath}"

        Call path.__INFO_ECHO
        Call New IORedirectFile(BIN, args).Run()

        If Not response Is Nothing Then
            Call response.SuccessMsg("Task complete!")
        End If

        Return True
    End Function
End Class
