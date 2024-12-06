import math

import matplotlib as mpl
import matplotlib.gridspec
import matplotlib.pyplot as plt
import numpy as np
import calculating
import reading


###############################################################
#                   Reachable Transparency                    #
###############################################################
def plot_error_transparency_relation():
    """
    This method plots the relationship between the error between the student and the Ghost, and the alpha value that this leads to.
    """
    min_transparency_settings = [0.2, 0.1, 0.05, 0]
    max_transparency_settings = [0.2, 0.2, 0.2, 0.2]

    # Plot the relationship between 100 error values between 0 and 1, and the transparency.
    errors = np.linspace(0, 1, 100)

    fig, ax = plt.subplots()

    for (min_alpha, max_alpha) in zip(min_transparency_settings, max_transparency_settings):
        alpha_values = calculating.calculate_alpha(errors, min_alpha, max_alpha)
        ax.plot(errors, alpha_values, label=f"min_alpha: {min_alpha}, max_alpha: {max_alpha}")
        print(f'Min transparency setting: {min_alpha}, Max transparency setting: {max_alpha}, Calculated Alpha Values: {alpha_values}')

    ax.set_xlabel("Error Value Between 0 and 1")
    ax.set_ylabel("Alpha (1 - transparency) Between 0 and 1")
    ax.set_title("Relationship between Error and Transparency")

    plt.legend()

    fig.savefig('Relationship between Error and Transparency.png')
    plt.show()


###############################################################
#                            Error                            #
###############################################################

def plot_distance_one_student(figure_title, time_points, euclid_distances_per_trial, dtw_distances):
    fig = plt.figure(figsize=plt.figaspect(0.4))
    fig.suptitle(figure_title)
    gs = matplotlib.gridspec.GridSpec(1, 2)

    ax1, ax2 = fig.add_subplot(gs[0]), fig.add_subplot(gs[1])

    # Add space between the subplots.
    fig.subplots_adjust(wspace=0.5)

    # Plot the Euclidean Distance
    colours = ["crimson", "mediumvioletred", "fuchsia", "orchid", "darkorchid", "blueviolet", "indigo", "mediumblue", "mediumslateblue", "cornflowerblue", "lightskyblue", "cyan", "paleturquoise"]

    for i in range(len(euclid_distances_per_trial)):
        euclid_distances = euclid_distances_per_trial[i]
        ax1.plot(time_points[i], euclid_distances, label=f"Trial {i + 1}", color=colours[i])

    ax1.set_xlabel("Time (s)")
    ax1.set_ylabel("Distance")
    ax1.set_xlim(0, 25.5)
    box = ax1.get_position()
    ax1.set_position([box.x0, box.y0, box.width * 0.9, box.height])
    ax1.set_title("Euclidean")

    # Plot the DTW Distance
    ax2.plot([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13], dtw_distances)
    ax2.set_title("Dynamic Time Warping")
    ax2.set_xlabel("Trial")
    ax2.set_ylabel("Distance")
    ax2.set_ylim(0, 8)
    ax2.set_xticks([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13])
    ax2.grid()
    ax2.axvline(10.5, color='grey')

    ax1.legend(loc="upper center", bbox_to_anchor=[1.2, 1])
    fig.savefig(f'{figure_title}.png')
    plt.show()


def plot_sanity_check(figure_title, euclid_distances_per_trial, dtw_distances):
    fig = plt.figure(figsize=plt.figaspect(0.4))
    fig.suptitle(figure_title)
    gs = matplotlib.gridspec.GridSpec(1, 2)

    ax1, ax2 = fig.add_subplot(gs[0]), fig.add_subplot(gs[1])

    i = 0

    # Add space between the subplots.
    fig.subplots_adjust(wspace=0.5)

    # Plot the Euclidean Distance
    colours = ["crimson", "mediumvioletred", "fuchsia", "orchid", "darkorchid", "blueviolet", "indigo", "mediumblue", "mediumslateblue", "cornflowerblue", "lightskyblue", "cyan", "paleturquoise"]

    dists = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
    for i in range(len(euclid_distances_per_trial)):
        dists[i] = np.sum(euclid_distances_per_trial[i])

    ax1.plot([0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12], dists)
    ax1.set_xlabel("Trial Index")
    ax1.set_ylabel("Distance")
    ax1.set_xlim(0, 13)
    box = ax1.get_position()
    ax1.set_position([box.x0, box.y0, box.width * 0.9, box.height])
    ax1.set_title("Euclidean")

    # Plot the DTW Distance
    ax2.plot([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13], dtw_distances)
    ax2.set_title("Dynamic Time Warping")
    ax2.set_xlabel("Trial")
    ax2.set_ylabel("Distance")
    ax2.set_ylim(0, 12)
    ax2.set_xticks([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13])
    ax2.grid()

    ax1.legend(loc="upper center", bbox_to_anchor=[1.2, 1])
    fig.savefig(f'{figure_title}.png')
    plt.show()


def plot_distance_averages(dynamic_euc_distances, dynamic_dtw_distances, static_euc_distances, static_dtw_distances):
    fig = plt.figure(figsize=plt.figaspect(0.4))
    fig.suptitle("Distance per Trial")
    gs = matplotlib.gridspec.GridSpec(1, 2)

    ax1, ax2 = fig.add_subplot(gs[0]), fig.add_subplot(gs[1])
    # Add space between the subplots.
    fig.subplots_adjust(wspace=0.5)

    # Obtain the mean and std of all arrays.
    d_euc = [np.mean(np.array(x)) for x in dynamic_euc_distances]
    d_dtw = [np.mean(np.array(x)) for x in dynamic_dtw_distances]
    s_euc = [np.mean(np.array(x)) for x in static_euc_distances]
    s_dtw = [np.mean(np.array(x)) for x in static_dtw_distances]

    std_d_euc = [np.std(np.array(x)) for x in dynamic_euc_distances]
    std_d_dtw = [np.std(np.array(x)) for x in dynamic_dtw_distances]
    std_s_euc = [np.std(np.array(x)) for x in static_euc_distances]
    std_s_dtw = [np.std(np.array(x)) for x in static_dtw_distances]

    print(f'D_EUC_MEAN: {d_euc}, D_EUC_STD: {std_d_euc}, D_DTW_MEAN: {d_dtw}, D_DTW_STD: {std_d_dtw}, S_EUC_MEAN: {s_euc}, S_EUC_STD: {std_s_euc}, S_DTW_MEAN: {s_dtw}, S_DTW_STD: {std_s_dtw}')

    # Plot the errors.
    trial_indexes = np.array([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13])
    bar_width = 0.4
    ax1.bar(trial_indexes - 0.5 * bar_width, d_euc, label="Dynamic", color="rebeccapurple", width=bar_width)
    ax1.bar(trial_indexes + 0.5 * bar_width, s_euc, label="Static", color="cornflowerblue", width=bar_width)

    ax2.bar(trial_indexes - 0.5 * bar_width, d_dtw, label="Dynamic", color="rebeccapurple", width=bar_width)
    ax2.bar(trial_indexes + 0.5 * bar_width, s_dtw, label="Static", color="cornflowerblue", width=bar_width)

    # Set the axis limits, and make space for the legend.
    ax1.set_xlim(0.5, 13.5)
    ax1.set_xlabel("Trial")
    box = ax1.get_position()
    ax1.set_position([box.x0, box.y0 + box.height * 0.2, box.width, box.height * 0.8])
    ax1.legend(loc="upper center", bbox_to_anchor=[0.5, -0.15], ncol=2)
    ax1.set_ylabel("Average Distance")

    ax2.set_xlim(0.5, 13.5)
    ax2.set_xlabel("Trial")
    box = ax2.get_position()
    ax2.set_position([box.x0, box.y0 + box.height * 0.2, box.width, box.height * 0.8])
    ax2.legend(loc="upper center", bbox_to_anchor=[0.5, -0.15], ncol=2)
    ax2.set_ylabel("Average Distance")

    ax1.axvline(10.5, color='grey')
    ax2.axvline(10.5, color='grey')

    # Plot error bars
    ax1.errorbar(trial_indexes - 0.2, d_euc, std_d_euc, fmt=".", color='black')
    ax1.errorbar(trial_indexes + 0.2, s_euc, std_s_euc, fmt=".", color='black')

    ax2.errorbar(trial_indexes - 0.2, d_dtw, std_d_dtw, fmt=".", color='black')
    ax2.errorbar(trial_indexes + 0.2, s_dtw, std_s_dtw, fmt=".", color='black')

    # Place the legend and set up axis ticks.
    ax1.set_xticks(trial_indexes)
    ax2.set_xticks(trial_indexes)
    ax1.set_title("Euclidean")
    ax2.set_title("Dynamic Time Warping")
    ax1.grid()
    ax2.grid()
    fig.savefig("DistanceMeansWithStandardDev.png")
    plt.show()

    return d_euc, d_dtw, s_euc, s_dtw, std_d_euc, std_d_dtw, std_s_euc, std_s_dtw


###############################################################
#               Completion Statistics                         #
###############################################################

def plot_num_participants(trial_indexes, num_dynamic_completed, num_static_completed):
    # Plot the number of completions.
    fig, ax = plt.subplots()
    bar_width = 0.4
    plt.bar(trial_indexes - 0.5 * bar_width, num_dynamic_completed, label="Dynamic Completed", color="mediumorchid", width=bar_width)
    plt.bar(trial_indexes + 0.5 * bar_width, num_static_completed, label="Static Completed", color="cornflowerblue", width=bar_width)

    # Set the axis limits, positions, and labels.
    ax.set_xlim(0.5, 13.5)
    ax.set_xlabel("Trial")
    box = ax.get_position()
    ax.set_position([box.x0, box.y0 + box.height * 0.2, box.width, box.height * 0.8])
    ax.set_ylabel("Number of Participants")
    ax.set_ylim(0, 10)

    ax.axvline(10.5, color='grey')

    # Set the title, the x-axis ticks, and place the legend.
    plt.title("Number of Completions per Group")
    plt.xticks([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13])
    plt.legend(loc="upper center", bbox_to_anchor=[0.5, -0.15], ncol=2)
    plt.grid()
    fig.savefig('Number of Completions per Group.png')
    plt.show()


def plot_completion(dynamic_completed, static_completed):
    # Obtain the mean for all non-empty arrays.
    d_c = [np.mean(np.array(x)) if len(x) > 0 else 0 for x in dynamic_completed]
    s_c = [np.mean(np.array(x)) if len(x) > 0 else 0 for x in static_completed]

    # Obtain the maximum standard deviation for all non-empty arrays.
    std_d_c = [np.std(np.array(x)) if len(x) > 0 else 0 for x in dynamic_completed]
    print(f"Maximum STD Dynamic Completed: {np.max(std_d_c)}")
    std_s_c = [np.std(np.array(x)) if len(x) > 0 else 0 for x in static_completed]
    print(f"Maximum STD Static Completed: {np.max(std_s_c)}")

    # Obtain the number of completions per group.
    num_d_c = [len(x) for x in dynamic_completed]
    num_s_c = [len(x) for x in static_completed]

    # Plot the number of completions.
    trial_indexes = np.array([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13])
    plot_num_participants(trial_indexes, num_d_c, num_s_c)

    # Plot the time to completion.
    fig, ax = plt.subplots()
    bar_width = 0.4
    plt.bar(trial_indexes - 0.5 * bar_width, d_c, label="Dynamic Completed", color="mediumorchid", width=bar_width)
    plt.bar(trial_indexes + 0.5 * bar_width, s_c, label="Static Completed", color="cornflowerblue", width=bar_width)

    # Set the axis limits, and make space for the legend.
    ax.set_xlim(0.5, 13.5)
    ax.set_ylim(0, 25.5)
    ax.set_xlabel("Trial")
    box = ax.get_position()
    ax.set_position([box.x0, box.y0 + box.height * 0.2, box.width, box.height * 0.8])
    ax.set_ylabel("Time To Completion (s)")

    ax.axvline(10.5, color='grey')

    # Place the legend and set up axis ticks.
    plt.title("Task Completion Time per Trial")
    plt.xticks([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13])
    plt.legend(loc="upper center", bbox_to_anchor=[0.5, -0.15], ncol=2)
    plt.grid()
    fig.savefig('Task Completion Time per Trial.png')
    plt.show()


###############################################################
#                        Transparency                         #
###############################################################

def plot_transparency_averages(transparency_per_trial):
    # Create the plot.
    fig, ax = plt.subplots()

    # Calculate the mean and standard deviations of the transparency per trial.
    means = [np.mean(np.array(a_list)) for a_list in transparency_per_trial]
    stds = [np.std(np.array(a_list)) for a_list in transparency_per_trial]

    # Plot the means as a bar chart per trial.
    ax.bar([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13], means, color="cornflowerblue")
    ax.set_xlabel("Trial")
    ax.set_ylabel("Alpha Value")
    ax.set_title("Mean Transparency of the Dynamically Transparent Ghost Instructor")

    # Add upper and lower transparency thresholds to the figure.
    xmin = np.array([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]) - 0.5
    xmax = np.array([2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14]) - 0.5
    y_lower = [0.2, 0.2, 0.2, 0.1, 0.1, 0.05, 0.05, 0, 0, 0, 0, 0, 0]
    y_upper = np.array([0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0, 0, 0])

    mod = calculating.compute_modifier(1, float(0.3))
    for i in range(0, len(y_upper)):
        y_upper[i] = y_lower[i] + mod * (y_upper[i] - y_lower[i])

    ax.hlines(y_lower,
              xmin=xmin, xmax=xmax,
              linestyles='--', color='cyan', label='lower threshold')
    ax.hlines(y_upper,
              xmin=xmin, xmax=xmax,
              linestyles='--', color='mediumslateblue', label='upper threshold')

    plt.axvline(x=10.5, color='grey')

    # Plot a standard-deviation error bar per bar.
    plt.errorbar([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13], means, stds, fmt=".", color='rebeccapurple')

    # Plot the legend, set the number of x-axis ticks, and show the plot.
    plt.legend(bbox_to_anchor=[0.75, 0.9])
    plt.xticks([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13])
    ax.set_xlim(0.5, 13.5)

    fig.savefig('Mean Transparency of the Dynamically Transparent Ghost Instructor.png')
    plt.show()


def plot_transparency_per_user(transparency_lists, figure_title, trial_indexes):
    # Create a figure with the right aspect ratio and axes.
    fig = plt.figure(figsize=plt.figaspect(0.4))
    fig.suptitle(figure_title)
    gs = matplotlib.gridspec.GridSpec(1, 3)

    ax1, ax2, ax3 = fig.add_subplot(gs[0]), fig.add_subplot(gs[1]), fig.add_subplot(gs[2])

    # Set up the array of colours that will be used for plotting the data.
    colours = ["cornflowerblue", "rebeccapurple", "cornflowerblue", "rebeccapurple", "cornflowerblue", "rebeccapurple", "grey"]

    # Add space between the subplots.
    fig.subplots_adjust(wspace=0.3)

    # Plot the transparency for trials 4 and 5 in the left plot, 6 and 7 in the middle plot, and 8-10 in the right plot.
    for i in range(0, len(transparency_lists)):
        ax = ax1 if trial_indexes[i] < 5 else ax2 if trial_indexes[i] < 7 else ax3
        ax.set_ylim(0, 0.2)

        alpha_list = transparency_lists[i]
        time, alpha = zip(*alpha_list)
        # Plot in seconds, not milliseconds.
        ax.plot(np.array(time) / 1000, alpha, color=colours[i], alpha=1, label=f"trial {trial_indexes[i] + 1}")

        ax.set_xlabel("Time (s)")
        ax.set_ylabel("Alpha Value")
        ax.legend()

    # Show the actual thresholds.
    mod = calculating.compute_modifier(1, float(0.3))
    # alpha = y_lower[i] + mod * (y_upper[i] - y_lower[i])

    ax1.hlines(y=0.1, xmin=0, xmax=25.5, linestyles='--', color='cyan', label='lower threshold')
    ax1.hlines(y=0.1 + mod * (0.2-0.1), xmin=0, xmax=25.5, linestyles='--', color='mediumslateblue', label='upper threshold')

    ax2.hlines(y=0.05, xmin=0, xmax=25.5, linestyles='--', color='cyan', label='lower threshold')
    ax2.hlines(y=0.05 + mod * (0.2-0.05), xmin=0, xmax=25.5, linestyles='--', color='mediumslateblue', label='upper threshold')

    ax3.hlines(y=0, xmin=0, xmax=25.5, linestyles='--', color='cyan', label='lower threshold')
    ax3.hlines(y=0 + mod * (0.2-0), xmin=0, xmax=25.5, linestyles='--', color='mediumslateblue', label='upper threshold')

    fig.savefig(f'{figure_title[:-30]}.png')
    plt.show()


###############################################################
#                           Student                           #
###############################################################

def plot_student_loc(student_pointer_pos, trial_index, student_num, dynamic):
    # Obtain the mean-shifted x, y, and z coordinates of the positions.
    time, xs, ys, zs = calculating.mean_shift_data(student_pointer_pos)
    positions = [time, xs, ys, zs]
    fig_title = dynamic + " Student " + str(student_num if student_num - 1 < 10 else student_num - 10) + ", Trial " + str(trial_index + 1) + ": The Position of the Right Pointer Finger"
    file_title = dynamic[0] + " " + str(student_num if student_num - 1 < 10 else student_num - 10) + ", t " + str(trial_index + 1) + " Pointer Pos"
    plot_2d_and_3d_points(positions, fig_title, file_title)


def plot_best_positions_one_student(first_train_file_loc, best_train_file_loc, best_test_file_loc, student_num, dynamic):
    # Obtain the positions arrays of the target trials.
    fig_title = dynamic + " Student " + str(student_num if student_num - 1 < 10 else student_num - 10) + ": The Position of the Right Pointer Finger"
    file_title = dynamic + " Student " + str(student_num if student_num - 1 < 10 else student_num - 10) + " Position"
    _, _, _, student_pointer_pos1, _ = reading.read_file(first_train_file_loc)
    _, _, _, student_pointer_pos2, _ = reading.read_file(best_train_file_loc)
    _, _, _, student_pointer_pos3, _ = reading.read_file(best_test_file_loc)

    time1, xs1, ys1, zs1 = calculating.mean_shift_data(student_pointer_pos1)
    positions_first_train = [time1, xs1, ys1, zs1]

    time2, xs2, ys2, zs2 = calculating.mean_shift_data(student_pointer_pos2)
    positions_best_train = [time2, xs2, ys2, zs2]

    time3, xs3, ys3, zs3 = calculating.mean_shift_data(student_pointer_pos3)
    positions_best_test = [time3, xs3, ys3, zs3]

    # Plot the 2D representations of the target trials.
    fig = plt.figure(figsize=(20, 7))
    fig.suptitle(fig_title)
    fig.tight_layout()

    gs = matplotlib.gridspec.GridSpec(1, 3)

    # Obtain the right colour map scale.
    min_time = 0
    max_time = min(max(time1), max(time2), max(time3))

    ax = fig.add_subplot(gs[0])
    ax.set_title("First Training Trial")
    plot_best_2d_points(positions_first_train, ax, min_time, max_time)
    ax = fig.add_subplot(gs[1])
    ax.set_title("Best Training Trial")
    plot_best_2d_points(positions_best_train, ax, min_time, max_time)
    ax = fig.add_subplot(gs[2])
    ax.set_title("Best Test Trial")
    plot_best_2d_points(positions_best_test, ax, min_time, max_time)

    # Plot a colour bar with the right scale.
    cmap = plt.get_cmap('cool')
    norm = mpl.colors.Normalize(vmin=min_time, vmax=max_time / 1000)
    sm = plt.cm.ScalarMappable(cmap=cmap, norm=norm)
    sm.set_array([])

    colour_bar_ax = fig.add_axes([.93, .09, .01, .8])
    plt.colorbar(sm, label="Point in Time (s)", pad=0.1, cax=colour_bar_ax)

    fig.savefig(f'{file_title}.png', format="png")
    plt.show()


###############################################################
#                            Ghost                            #
###############################################################

def plot_ghost_loc(ghost_pointer_pos):
    # Obtain the mean-shifted x, y, and z coordinates of the positions.
    time, xs, ys, zs = calculating.mean_shift_data(ghost_pointer_pos)
    positions = [time, xs, ys, zs]
    fig_title = "The Position of the Ghost Instructor's Right Pointer Finger"
    plot_2d_and_3d_points(positions, fig_title, fig_title)


###############################################################
#                        Plot Points                          #
###############################################################

def plot_2d_and_3d_points(point_array, figure_title, file_title):
    fig = plt.figure(figsize=(15, 7))
    fig.suptitle(figure_title)
    fig.tight_layout()

    gs = matplotlib.gridspec.GridSpec(1, 2, width_ratios=[2, 3])

    ax = fig.add_subplot(gs[0])
    plot_2d_points(point_array, ax)

    ax = fig.add_subplot(gs[1], projection='3d')
    ax.set_zlabel("Y Coordinate", labelpad=8)
    plot_3d_points(point_array, ax)

    # Plot a colour bar with the right scale.
    cmap = plt.get_cmap('cool')
    norm = mpl.colors.Normalize(vmin=0, vmax=point_array[0][-1] / 1000)
    sm = plt.cm.ScalarMappable(cmap=cmap, norm=norm)
    sm.set_array([])

    colour_bar_ax = fig.add_axes([.94, .09, .01, .8])
    plt.colorbar(sm, label="Point in Time (s)", pad=0.1, cax=colour_bar_ax)

    fig.savefig(f'{file_title}.png')
    plt.show()


def plot_3d_points(point_array, ax):
    # Separate the coordinates of the points.
    time, xs, ys, zs = point_array[0], point_array[1], point_array[2], point_array[3]

    # Scatter the points using the 'cool' colour map.
    cmap = plt.get_cmap('cool')
    ax.scatter(xs, zs, ys, c=time, cmap=cmap, marker=",", s=15)

    # Set the plot labels
    ax.set_xlabel("X Coordinate", labelpad=6)
    ax.set_ylabel("Z Coordinate", labelpad=6)
    ax.set_xticks([-0.4, -0.3, -0.2, -0.1, 0, 0.1, 0.2, 0.3, 0.4])
    ax.set_zticks([-0.4, -0.3, -0.2, -0.1, 0, 0.1, 0.2, 0.3])

    # Add a title.
    ax.set_title("3D")


def plot_2d_points(point_array, ax):
    # Separate the coordinates of the points.
    time, xs, ys = point_array[0], point_array[1], point_array[2]

    # Scatter the points using the 'cool' colour map.
    cmap = plt.get_cmap('cool')
    ax.scatter(xs, ys, c=time, cmap=cmap, marker=",", s=15)

    # Set the plot labels
    ax.set_xlabel("X Coordinate", labelpad=6)
    ax.set_ylabel("Y Coordinate", labelpad=6)

    ax.set_xticks([-0.4, -0.3, -0.2, -0.1, 0, 0.1, 0.2, 0.3, 0.4])
    ax.set_yticks([-0.4, -0.3, -0.2, -0.1, 0, 0.1, 0.2, 0.3])
    ax.set_aspect('equal', adjustable='box')

    # Add a title.
    ax.grid(True)
    ax.set_title("2D")


def plot_best_2d_points(point_array, ax, vmin, vmax):
    # Separate the coordinates of the points.
    time, xs, ys = point_array[0], point_array[1], point_array[2]

    # Scatter the points using the 'cool' colour map.
    cmap = plt.get_cmap('cool')
    ax.scatter(xs, ys, c=time, cmap=cmap, marker=",", vmin=vmin, vmax=vmax, s=15)

    # Set the plot labels
    ax.set_xlabel("X Coordinate", labelpad=6)
    ax.set_ylabel("Y Coordinate", labelpad=6)

    ax.set_xticks([-0.4, -0.3, -0.2, -0.1, 0, 0.1, 0.2, 0.3, 0.4])
    ax.set_yticks([-0.4, -0.3, -0.2, -0.1, 0, 0.1, 0.2, 0.3])
    ax.set_aspect('equal', adjustable='box')

    # Add a title.
    ax.grid(True)


###############################################################
#                   Bezier Curve Plotting                     #
###############################################################


def cubic_bezier_curve(t):
    p1 = np.array([0, 0])
    p2 = np.array([2, 2])
    c1 = np.array([0.5, 4])
    c2 = np.array([1.5, -2])
    return math.pow(1-t, 3) * p1 + math.pow(1-t, 2) * t * 3 * c1 + 3 * t * t * (1-t) * c2 + math.pow(t, 3) * p2


def plot_number_of_t_values(num_t_values, ax, marker, offset, colour):
    t_values = np.linspace(0, 1, num_t_values)
    xs = []
    ys = []

    for t in t_values:
        point = cubic_bezier_curve(t) + offset
        xs.append(point[0])
        ys.append(point[1])

    ax.scatter(xs, ys, label=f"{num_t_values} t-values; offset: {offset}", marker=marker, color=colour)


def plot_bezier_curve():
    fig, ax = plt.subplots()

    plot_number_of_t_values(100, ax, "v", [0, 0.6], "rebeccapurple")
    plot_number_of_t_values(50, ax, "^", [0, 0.2], "mediumorchid")
    plot_number_of_t_values(10, ax, "<", [0, -0.2], "mediumslateblue")

    ax.set_xlabel("x-axis")
    ax.set_ylabel("y-axis")

    plt.title("The Relationship between Evenly Spaced t-Values and Bézier Curve Points.")
    plt.legend(loc="lower right")

    fig.savefig('The Relationship between Evenly Spaced t-Values and Bézier Curve Points.png')
    plt.show()
