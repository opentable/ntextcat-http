param($installPath, $toolsPath, $package, $project)

$configFile = $project.ProjectItems.Item("Core14.profile.xml")
$configFileCopyToOutput = $configFile.Properties.Item("CopyToOutputDirectory")
$configFileCopyToOutput.Value = 1

$webPage = $project.ProjectItems.Item("Views").ProjectItems.Item("language-detection-test-page.html")
$webPageCopyToOutput = $webPage.Properties.Item("CopyToOutputDirectory")
$webPageCopyToOutput.Value = 1


$mappingFile = $project.ProjectItems.Item("ISOCodeLookup.txt")
$mappingFileCopyToOutput = $mappingFile.Properties.Item("CopyToOutputDirectory")
$mappingFileCopyToOutput.Value = 1