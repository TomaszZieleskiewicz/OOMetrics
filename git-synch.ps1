$mainBranch = "main"
$currentBranch = git.exe branch --show-current
git.exe checkout $mainBranch --
git.exe pull --progress -v --no-rebase "origin"
git.exe checkout $currentBranch --
git.exe merge $mainBranch
git.exe push --progress "origin" ${currentBranch}:${currentBranch}