param($installPath, $toolsPath, $package, $project)

$configFile = $project.ProjectItems.Item("Core14.profile.xml")
$copyToOutput2 = $configFile.Properties.Item("CopyToOutputDirectory")
$copyToOutput2.Value = 1

$webPage = $project.ProjectItems.Item("Views").ProjectItems.Item("language-detection-test-page.html")
$copyToOutput1 = $webPage.Properties.Item("CopyToOutputDirectory")
$copyToOutput1.Value = 1