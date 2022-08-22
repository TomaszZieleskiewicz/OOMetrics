git.exe checkout main --
git.exe pull --progress -v --no-rebase "origin"
git.exe checkout develop --
git.exe merge main
git.exe push --progress "origin" develop:develop