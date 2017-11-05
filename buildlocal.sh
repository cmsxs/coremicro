rm -rf _builds _steps _projects _cache _temp
wercker build --git-domain github.com \
   --git-owner cmsxs \
   --git-repository coremicro
rm -rf _builds _steps _projects _cache _temp